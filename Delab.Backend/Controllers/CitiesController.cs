using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

[Route("api/cities")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly DataContext _context;
    public CitiesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        var listCities = await _context.Cities.OrderBy(x => x.Name).ToListAsync();
        return Ok(listCities);
    }

    [HttpGet("{idCity}")]
    public async Task<ActionResult<City>> GetCity(int idCity)
    {
        var city = await _context.Cities.FindAsync(idCity);
        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> PostCity([FromBody] City modelo)
    {
        try
        {
            _context.Cities.Add(modelo);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe un registro con el mismo nombre.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }

    [HttpPut]
    public async Task<ActionResult<City>> PutCity([FromBody] City modelo)
    {
        try
        {
            var UpdateCity = await _context.Cities.FirstOrDefaultAsync(x => x.CityId == modelo.CityId);

            if (UpdateCity == null)
            {
                return NotFound();
            }

            UpdateCity.Name = modelo.Name;
            UpdateCity.StateId = modelo.StateId;
            _context.Cities.Update(UpdateCity);
            await _context.SaveChangesAsync();

            return Ok(UpdateCity);
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe un registro con el mismo nombre.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{idCity}")]
    public async Task<IActionResult> DeleteCity(int idCity)
    {
        try
        {
            var city = await _context.Cities.FindAsync(idCity);

            if (city == null)
            {
                return BadRequest("No se encontro el registro para borrar");
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("REFERENCE"))
            {
                return BadRequest("No puede eliminar el registro porque tiene datos relacionados.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
}
