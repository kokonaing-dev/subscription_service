using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using subscription_service.Data;
using subscription_service.Models;

namespace subscription_service.Controllers;

[Route("odata/gateway-raw-events")]
public class GatewayRawEventController : ODataController
{
    private readonly AppDbContext _context;

    public GatewayRawEventController(AppDbContext context)
    {
        _context = context;
    }

    // GET collection
    [HttpGet]
    [EnableQuery]
    public IQueryable<GatewayRawEvent> Get(
        [FromQuery(Name = "$filter")] string filter = null,
        [FromQuery(Name = "$expand")] string expand = null,
        [FromQuery(Name = "$select")] string select = null,
        [FromQuery(Name = "$orderby")] string orderby = null,
        [FromQuery(Name = "$top")] int? top = null,
        [FromQuery(Name = "$skip")] int? skip = null)
    {
        return _context.GatewayRawEvents;
    }

    // GET single
    [HttpGet("{key}")]
    [EnableQuery]
    public SingleResult<GatewayRawEvent> Get([FromRoute] Guid key)
    {
        return SingleResult.Create(_context.GatewayRawEvents.Where(e => e.Id == key));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GatewayRawEvent entity)
    {
        if (entity == null)
        {
            return BadRequest("Request body is null or invalid.");
        }
        _context.GatewayRawEvents.Add(entity);
        await _context.SaveChangesAsync();
        return Created(entity);
    }

    // PATCH
    [HttpPatch("{key}")]
    public async Task<IActionResult> Patch([FromRoute] Guid key, [FromBody] Delta<GatewayRawEvent> delta)
    {
        var entity = await _context.GatewayRawEvents.FindAsync(key);
        if (entity == null)
        {
            return NotFound();
        }

        delta.Patch(entity);
        await _context.SaveChangesAsync();

        return Updated(entity);
    }

    // DELETE
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromRoute] Guid key)
    {
        var entity = await _context.GatewayRawEvents.FindAsync(key);
        if (entity == null)
        {
            return NotFound();
        }

        _context.GatewayRawEvents.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}


