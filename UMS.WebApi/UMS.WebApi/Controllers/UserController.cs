using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Owin.Logging;
using UMS.Application.DTOs;
using UMS.Application.Entities.Roles.Queries.GetRoleById;
using UMS.Application.Entities.Users.Commands.AddUser;
using UMS.Application.Entities.Users.Commands.RemoveUser;
using UMS.Application.Entities.Users.Commands.UpdateUser;
using UMS.Application.Entities.Users.Queries.GetUserById;
using UMS.Application.Entities.Users.Queries.GetUsers;
using UMS.Domain.Models;
using ILogger = Microsoft.Owin.Logging.ILogger;
using ILoggerFactory = Microsoft.Owin.Logging.ILoggerFactory;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet()]
    public async Task<List<UserDTO>> GetRoles()
    {
        // _logger.WriteInformation("asa");
        var result = await _mediator.Send(new GetUsersQuery());
        return  result;
    }
    
    [HttpGet("{id}")]
    public async Task<UserDTO> GetUserById([Required]long id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery()
        {
            Id = id
        });
        return (result);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> AddUser([FromBody] AddUserCommand addUserCommand)
    {
        var result = _mediator.Send(addUserCommand);
        return Ok(result);
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> RemoveUser([FromBody] RemoveUserCommand removeUserCommand)
    {
        if (await _mediator.Send(removeUserCommand))
        {
            return Ok("User deleted successfully");
        }
        else
        {
            return BadRequest("User not found!!");
        }
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateRole(UpdateUserCommand updateUserCommand)
    {
        var existUser = await _mediator.Send(new GetUserByIdQuery()
        {
            Id = updateUserCommand.Id
        });

        if (existUser==null)
        {
            return BadRequest($"No user found with the id {updateUserCommand.Id}!!");
        }
        var result = await _mediator.Send(updateUserCommand);

        return Ok(result);
    }

}