using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

[Route("api/states")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[ApiController]
public class StatesController : ControllerBase
{
    private readonly DataContext _context;
    public StatesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<State>>> GetStates()
    {
        var listStates = await _context.States.OrderBy(x => x.Name).ToListAsync();
        return Ok(listStates);
    }

    [HttpGet("{idState}")]
    public async Task<ActionResult<State>> GetState(int idState)
    {
        var state = await _context.States.FindAsync(idState);
        return Ok(state);
    }

    [HttpPost]
    public async Task<IActionResult> PostState([FromBody] State modelo)
    {
        try
        {
            _context.States.Add(modelo);
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
    public async Task<ActionResult<State>> PutState([FromBody] State modelo)
    {
        try
        {
            var UpdateState = await _context.States.FirstOrDefaultAsync(x => x.StateId == modelo.StateId);

            if (UpdateState == null)
            {
                return NotFound();
            }

            UpdateState.Name = modelo.Name;
            UpdateState.CountryId = modelo.CountryId;
            _context.States.Update(UpdateState);
            await _context.SaveChangesAsync();

            return Ok(UpdateState);
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

    [HttpDelete("{idState}")]
    public async Task<IActionResult> DeleteState(int idState)
    {
        try
        {
            var state = await _context.States.FindAsync(idState);

            if (state == null)
            {
                return BadRequest("No se encontro el registro para borrar");
            }

            _context.States.Remove(state);
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
