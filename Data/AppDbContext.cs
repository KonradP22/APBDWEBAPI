using Microsoft.EntityFrameworkCore;
using WebApplication1.models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors {get; set;}
    public DbSet<Prescription> Prescriptions {get; set;}
    public DbSet<Medicament> Medicaments {get; set;}
    public DbSet<Patient> Patients {get; set;}
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments {get; set;}

    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }    
    
}