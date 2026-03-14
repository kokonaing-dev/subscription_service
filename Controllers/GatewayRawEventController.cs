using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using subscription_service.DTOs;
using subscription_service.Models;
using subscription_service.Services;

namespace subscription_service.Controllers;

[Route("odata/gateway-raw-events")]
public class GatewayRawEventController : ODataController
{
    private readonly IGatewayRawEventService _service;

    public GatewayRawEventController(IGatewayRawEventService service)
    {
        _service = service;
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
        return _service.Query();
    }

    // GET single
    [HttpGet("{key}")]
    [EnableQuery]
    public SingleResult<GatewayRawEvent> Get([FromRoute] Guid key)
    {
        return SingleResult.Create(_service.Query().Where(e => e.Id == key));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GatewayRawEventCreateDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Request body is null or invalid.");
        }
        var entity = await _service.CreateAsync(dto);
        return Created(entity);
    }

    // PATCH
    [HttpPatch("{key}")]
    public async Task<IActionResult> Patch([FromRoute] Guid key, [FromBody] Delta<GatewayRawEvent> delta)
    {
        if (delta == null)
        {
            return BadRequest("Request body is null or invalid.");
        }

        var entity = await _service.PatchAsync(key, delta);
        if (entity == null)
        {
            return NotFound();
        }

        return Updated(entity);
    }

    // DELETE
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromRoute] Guid key)
    {
        var deleted = await _service.DeleteAsync(key);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}


