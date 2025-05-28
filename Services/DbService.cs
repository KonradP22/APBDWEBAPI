using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;

public interface IDbService
{
    public Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescription);
    public Task<PrescriptionGetDto> GetPrescriptionByIdAsync(int id);
    public Task<PatientGetDto> GetPatientDetailsByIdAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescription)
    {
        if (prescription.DueDate < prescription.Date)
        {
            throw new Exception("Due date cannot be earlier than date");
        }

        if (prescription.Medicaments.Count > 10)
        {
            throw new Exception("Medicaments count is too large");
        }

        List<Medicament> medicaments = [];

        foreach (var med in prescription.Medicaments)
        {
            var medicament = await data.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == med.MedicamentId);
            if (medicament is null)
            {
                throw new Exception("Medicament not found");
            }
            medicaments.Add(medicament);
        }
        
        var Patient = await data.Patients.FirstOrDefaultAsync(p => p.FirstName == prescription.Patient.FirstName && p.LastName == prescription.Patient.LastName && p.BirthDate == prescription.Patient.BirthDate);

        if (Patient is null)
        {
            Patient = new Patient
            {
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate,
            };
            data.Patients.Add(Patient);
            await data.SaveChangesAsync();
        }
        
        var doctor = await data.Doctors.FirstOrDefaultAsync(d => d.FirstName == prescription.Doctor.FirstName && d.LastName == prescription.Doctor.LastName && d.Email == prescription.Doctor.Email);

        if (doctor is null)
        {
            throw new Exception("Doctor not found");
        }


        var newPrescription = new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = Patient.IdPatient,
            PrescriptionMedicaments = prescription.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.MedicamentId,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };
        
        data.Prescriptions.Add(newPrescription);
        await data.SaveChangesAsync();

        return new PrescriptionGetDto
        {
            Id = newPrescription.IdPrescription,
            Date = newPrescription.Date,
            DueDate = newPrescription.DueDate,
            
            Patient = new PatientDto
            {
                IdPatient = newPrescription.IdPatient,
                FirstName = newPrescription.Patient.FirstName,
                LastName = newPrescription.Patient.LastName,
                BirthDate = newPrescription.Patient.BirthDate,
            },
            
            Medicaments = newPrescription.PrescriptionMedicaments.Select(pm => new PrescriptionGetDtoMedicament
            {
                IdMedicament = pm.IdMedicament,
                Name = pm.Medicament.Name,
                Description = pm.Medicament.Description,
                Dose = pm.Dose,
            }).ToList()
            
        };
        
        
    }

    public async Task<PrescriptionGetDto> GetPrescriptionByIdAsync(int id)
    {
        var prescription = await data.Prescriptions.Select(p => new PrescriptionGetDto
        {
            Id = p.IdPrescription,
            Date = p.Date,
            DueDate = p.DueDate,
            
            Patient = new PatientDto
            {
                IdPatient = p.Patient.IdPatient,
                FirstName = p.Patient.FirstName,
                LastName =  p.Patient.LastName,
                BirthDate = p.Patient.BirthDate,
            },
            
            Medicaments = p.PrescriptionMedicaments.Select(pm => new PrescriptionGetDtoMedicament
            {
                IdMedicament = pm.IdMedicament,
                Name = pm.Medicament.Name,
                Description = pm.Medicament.Description,
                Dose = pm.Dose,
            }).ToList(),
            
        }).FirstOrDefaultAsync(p => p.Id == id);
        if (prescription is null)
        {
            throw new Exception("Prescription not found");
        }
        return prescription;
    }

    public async Task<PatientGetDto> GetPatientDetailsByIdAsync(int id)
    {
        var result = await data.Patients.Select(p => new PatientGetDto
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            BirthDate = p.BirthDate,
            
            Prescriptions = p.Prescriptions.OrderBy(r => r.DueDate).Select(r => new PrescriptionDtoForPatient
            {
                IdPrescription = r.IdPrescription,
                Date = r.Date,
                DueDate = r.DueDate,
                Medicaments = r.PrescriptionMedicaments.Select(pm => new PrescriptionGetDtoMedicament
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Dose = pm.Dose,
                }).ToList(),
                Doctor = new DoctorDto
                {
                    FirstName = r.Doctor.FirstName,
                    LastName = r.Doctor.LastName,
                    Email = r.Doctor.Email,
                }
            }).ToList()
        }).FirstOrDefaultAsync(p => p.IdPatient == id);
        
        return result ?? throw new Exception("Patient not found");
    }
}