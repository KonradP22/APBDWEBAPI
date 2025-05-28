using System.ComponentModel.DataAnnotations;
using WebApplication1.models;

namespace WebApplication1.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public DoctorDto Doctor { get; set; }
    
    [Required]
    public PatientDto Patient { get; set; }
    
    public List<PrescriptionMedicamentDto> Medicaments { get; set; }
}