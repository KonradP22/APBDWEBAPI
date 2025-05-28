using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class DoctorDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
}