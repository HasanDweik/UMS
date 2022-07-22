using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UMS.Domain.Models;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("odata/[controller]")]
public class RoleODController : ODataController
{
    private readonly UmsContext _context;
    private readonly ILogger<RoleODController> _logger;

    public RoleODController(UmsContext context, ILogger<RoleODController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [EnableQuery(PageSize = 15)]
    [HttpGet]
    public IQueryable<Role> GetR()
    {
        return _context.Roles;
    }
    
    [EnableQuery]
    [HttpGet("one")]
    public SingleResult<Role> GetOne([FromODataUri] long key)
    {
        var result = _context.Roles.Where(c => c.Id == key);
        return SingleResult.Create(result);
    }
    
    
    // [EnableQuery]
    // public async Task<IActionResult> Post([FromBody] Role role)
    // {
    //     _context.Roles.Add(role);
    //     await _context.SaveChangesAsync();
    //     return Created(role);
    // }
    //
    //
    // [EnableQuery]
    // public async Task<IActionResult> Delete([FromODataUri] long key)
    // {
    //     Role existingRole = await _context.Roles.FindAsync(key);
    //     if (existingRole == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     _context.Roles.Remove(existingRole);
    //     await _context.SaveChangesAsync();
    //     return StatusCode(StatusCodes.Status204NoContent);
    // }
    //
    // private bool RoleExisting(long key)
    // {
    //     return _context.Roles.Any(p => p.Id == key);
    // }
}