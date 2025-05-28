using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class PatientDto
{
    public int IdPatient { get; set; }
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
}