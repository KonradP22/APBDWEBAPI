namespace WebApplication1.DTOs;

public class PrescriptionDtoForPatient
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PrescriptionGetDtoMedicament> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
}