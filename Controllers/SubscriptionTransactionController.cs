using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using subscription_service.Data;
using subscription_service.Models;

namespace subscription_service.Controllers;

[Route("odata/subscription-transactions")]
public class SubscriptionTransactionController : ODataController
{
    private readonly AppDbContext _context;

    public SubscriptionTransactionController(AppDbContext context)
    {
        _context = context;
    }

    // GET collection
    [HttpGet]
    [EnableQuery]
    public IQueryable<SubscriptionTransaction> Get(
        [FromQuery(Name = "$filter")] string filter = null,
        [FromQuery(Name = "$expand")] string expand = null,
        [FromQuery(Name = "$select")] string select = null,
        [FromQuery(Name = "$orderby")] string orderby = null,
        [FromQuery(Name = "$top")] int? top = null,
        [FromQuery(Name = "$skip")] int? skip = null)
    {
        return _context.SubscriptionTransactions;
    }

    // GET single
    [HttpGet("{key}")]
    [EnableQuery]
    public SingleResult<SubscriptionTransaction> Get([FromRoute] Guid key)
    {
        return SingleResult.Create(_context.SubscriptionTransactions.Where(e => e.Id == key));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SubscriptionTransaction entity)
    {
        if (entity == null)
        {
            return BadRequest("Request body is null or invalid.");
        }
        _context.SubscriptionTransactions.Add(entity);
        await _context.SaveChangesAsync();
        return Created(entity);
    }

    // PATCH
    [HttpPatch("{key}")]
    public async Task<IActionResult> Patch([FromRoute] Guid key, [FromBody] Delta<SubscriptionTransaction> delta)
    {
        var entity = await _context.SubscriptionTransactions.FindAsync(key);
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
        var entity = await _context.SubscriptionTransactions.FindAsync(key);
        if (entity == null)
        {
            return NotFound();
        }

        _context.SubscriptionTransactions.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}


