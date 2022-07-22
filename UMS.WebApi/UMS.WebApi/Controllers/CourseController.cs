using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Entities.Courses.Commands.AddCourse;
using UMS.Application.Entities.Courses.Commands.RemoveCourse;
using UMS.Application.Entities.Courses.Commands.UpdateCourse;
using UMS.Application.Entities.Courses.Queries.GetCourseById;
using UMS.Application.Entities.Courses.Queries.GetCourses;
using UMS.Application.Entities.Roles.Commands.RemoveRole;
using UMS.Application.Entities.Roles.Queries.GetRoleById;
using UMS.Application.Entities.Roles.Queries.GetRoles;
using UMS.Domain.Models;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CourseController:Controller
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet()]
    public async Task<List<Course>> GetCourses()
    {
        var result = await _mediator.Send(new GetCoursesQuery());
        return  result;
    }
    
    [HttpGet("{id}")]
    public async Task<Course> GetCourseById([Required]long id)
    {
        var result = await _mediator.Send(new GetCourseByIdQuery()
        {
            Id = id
        });
        return (result);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> AddRole(long userRole, AddCourseCommand addCourseCommand)
    {
        if (userRole == 1)
        {
            var result = await _mediator.Send(addCourseCommand);
            return Ok(result);
        }

        return BadRequest("The user must be an admin to add a course");
    }
    
     [HttpDelete("Delete")]
     public async Task<IActionResult> DeleteCourse(RemoveCourseCommand removeCourseCommand)
     {

         if (await _mediator.Send(removeCourseCommand))
         {
             return Ok("Course deleted successfully");
         }
         else
         {
             return BadRequest("Course not found!!");
         }

     }
    
     [HttpPut("Update")]
     public async Task<IActionResult> UpdateCourse(UpdateCourseCommand updateCourseCommand)
     {
         var existRole = await _mediator.Send(new GetRoleByIdQuery()
         {
             Id = updateCourseCommand.Id
         });

         if (existRole==null)
         {
             return BadRequest($"No course found with the id {updateCourseCommand.Id}!!");
         }
         var result = await _mediator.Send(updateCourseCommand);

         return Ok(result);
     }
    
}