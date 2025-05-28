using WebApplication1.models;

namespace WebApplication1.DTOs;

public class PrescriptionGetDto
{
    public int Id { get; set; }
    public PatientDto Patient { get; set; }
    public List<PrescriptionGetDtoMedicament> Medicaments { get; set; }
    
   public DateTime Date { get; set; }
   public DateTime DueDate { get; set; }
}