using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Entities.TeacherPerCoursePerSessionTime.Commands.AddTeacherPerCoursePerSessionTime;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherPerCoursePerSessionTimeController : Controller
{
    private readonly IMediator _mediator;


    public TeacherPerCoursePerSessionTimeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> AddTeacherPerCoursePerSessionTime([FromBody]AddTeacherPerCoursePerSessionTimeCommand addTeacherPerCoursePerSessionTimeCommand)
    {
        var result =  await _mediator.Send(addTeacherPerCoursePerSessionTimeCommand);
        return Ok(result);
    }
}