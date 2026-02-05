using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        var listCountry = await _context.Countries
            .Include(x => x.States)!
            .ThenInclude(x => x.Cities)
            .OrderBy(x => x.Name).ToListAsync();
        return Ok(listCountry);
    }

    [HttpGet("{idCountry}")]
    public async Task<ActionResult<Country>> GetCountry(int idCountry)
    {
        var country = await _context.Countries.FindAsync(idCountry);
        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> PostCountry([FromBody] Country modelo)
    {
        try
        {
            _context.Countries.Add(modelo);
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
    public async Task<ActionResult<Country>> PutCountry([FromBody] Country modelo)
    {
        try
        {
            var UpdateContry = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == modelo.CountryId);

            if (UpdateContry == null)
            {
                return NotFound();
            }

            UpdateContry.Name = modelo.Name;
            UpdateContry.CodPhone = modelo.CodPhone;
            _context.Countries.Update(UpdateContry);
            await _context.SaveChangesAsync();

            return Ok(UpdateContry);
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

    [HttpDelete("{idCountry}")]
    public async Task<IActionResult> DeleteCountry(int idCountry)
    {
        try
        {
            var country = await _context.Countries.FindAsync(idCountry);

            if (country == null)
            {
                return BadRequest("No se encontro el registro para borrar");
            }

            _context.Countries.Remove(country);
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
