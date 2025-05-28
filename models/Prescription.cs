using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.models;

[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }

    public int IdPatient { get; set; }
    
    public int IdDoctor { get; set; }
    
    [ForeignKey("IdDoctor")]
    public virtual Doctor Doctor { get; set; } = null!;
    
    [ForeignKey("IdPatient")]
    public virtual Patient Patient { get; set; } = null!;
    
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
    

}