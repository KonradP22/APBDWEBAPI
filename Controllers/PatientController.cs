using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IDbService service) : ControllerBase 
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetPatientDetailsByIdAsync(id));
        }
        catch(Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
}