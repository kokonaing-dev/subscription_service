using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using subscription_service.Data;
using subscription_service.Models;

namespace subscription_service.Controllers;

[Route("odata/plans")]
public class PlanController : ODataController
{
    private readonly AppDbContext _context;

    public PlanController(AppDbContext context)
    {
        _context = context;
    }


    // GET collection
    [HttpGet]
    [EnableQuery]
    public IQueryable<Plan> Get(
        [FromQuery(Name = "$filter")] string filter = null,
        [FromQuery(Name = "$expand")] string expand = null,
        [FromQuery(Name = "$select")] string select = null,
        [FromQuery(Name = "$orderby")] string orderby = null,
        [FromQuery(Name = "$top")] int? top = null,
        [FromQuery(Name = "$skip")] int? skip = null)
    {
        return _context.Plans;
    }

    // GET single
    [HttpGet("{key}")]
    [EnableQuery]
    public SingleResult<Plan> Get([FromRoute] Guid key)
    {
        return SingleResult.Create(_context.Plans.Where(p => p.Id == key));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Plan plan)
    {
        if (plan == null)
        {
            return BadRequest("Request body could not be deserialized. Ensure Content-Type is application/json and the JSON matches the Plan model.");
        }

        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();
        return Created(plan);
    }

    // PATCH
    [HttpPatch("{key}")]
    public async Task<IActionResult> Patch([FromRoute] Guid key, [FromBody] Delta<Plan> delta)
    {
        var entity = await _context.Plans.FindAsync(key);
        if (entity == null) return NotFound();
        delta.Patch(entity);
        await _context.SaveChangesAsync();
        return Updated(entity);
    }

    // DELETE
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromRoute] Guid key)
    {
        var entity = await _context.Plans.FindAsync(key);
        if (entity == null) return NotFound();
        _context.Plans.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
