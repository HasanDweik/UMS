using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Entities.ClassEnrollment.Commands.AddClassEnrollment;

namespace UMS.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ClassEnrollmentController : Controller
{
    private readonly IMediator _mediator;

    public ClassEnrollmentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> AddClassEnrollment([FromBody] AddClassEnrollmentCommand addClassEnrollmentCommand)
    {
        var result =  await _mediator.Send(addClassEnrollmentCommand);
        if (result == null)
        {
            return BadRequest("The user must be a teacher to be assigned to a course!!!");
        }
        return Ok(result);
    }
}