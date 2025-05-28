using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPrescriptionDetails([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetPrescriptionByIdAsync(id));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateDto prescriptionData)
    {
        try
        {
            var result = await service.CreatePrescriptionAsync(prescriptionData);
            return CreatedAtAction(nameof(GetPrescriptionDetails), new { id = result.Id }, result);
        }
        catch (Exception e) 
        {
            return NotFound(e.Message);
        }
    }
    
}