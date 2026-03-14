using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using subscription_service.Data;
using subscription_service.Models;

namespace subscription_service.Controllers;

[Route("odata/ai-point-transactions")]
public class AIPointTransactionController : ODataController
{
    private readonly AppDbContext _context;

    public AIPointTransactionController(AppDbContext context)
    {
        _context = context;
    }

    // GET collection
    [HttpGet]
    [EnableQuery]
    public IQueryable<AIPointTransaction> Get(
        [FromQuery(Name = "$filter")] string filter = null,
        [FromQuery(Name = "$expand")] string expand = null,
        [FromQuery(Name = "$select")] string select = null,
        [FromQuery(Name = "$orderby")] string orderby = null,
        [FromQuery(Name = "$top")] int? top = null,
        [FromQuery(Name = "$skip")] int? skip = null)
    {
        return _context.AIPointTransactions;
    }

    // GET single
    [HttpGet("{key}")]
    [EnableQuery]
    public SingleResult<AIPointTransaction> Get([FromRoute] Guid key)
    {
        return SingleResult.Create(_context.AIPointTransactions.Where(e => e.Id == key));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AIPointTransaction entity)
    {
        if (entity == null)
        {
            return BadRequest("Request body is null or invalid.");
        }
        _context.AIPointTransactions.Add(entity);
        await _context.SaveChangesAsync();
        return Created(entity);
    }

    // PATCH
    [HttpPatch("{key}")]
    public async Task<IActionResult> Patch([FromRoute] Guid key, [FromBody] Delta<AIPointTransaction> delta)
    {
        var entity = await _context.AIPointTransactions.FindAsync(key);
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
        var entity = await _context.AIPointTransactions.FindAsync(key);
        if (entity == null)
        {
            return NotFound();
        }

        _context.AIPointTransactions.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}


