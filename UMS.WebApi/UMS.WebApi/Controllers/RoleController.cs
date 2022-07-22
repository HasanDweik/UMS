using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.DTOs;
using UMS.Application.Entities.Roles.Commands.AddRole;
using UMS.Application.Entities.Roles.Commands.RemoveRole;
using UMS.Application.Entities.Roles.Commands.UpdateRole;
using UMS.Application.Entities.Roles.Queries.GetRoleById;
using UMS.Application.Entities.Roles.Queries.GetRoles;
using UMS.Domain.Models;
using UMS.Infrastructure.Abstraction.Services;

namespace UMS.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]

public class RoleController : Controller
{
    private readonly IMediator _mediator;


    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet()]
    public async Task<List<Role>> GetRoles()
    {
        var result = await _mediator.Send(new GetRolesQuery());
        return  result;
    }
    
    [HttpGet("{id}")]
    public async Task<Role> GetRoleById([Required]long id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery()
        {
            Id = id
        });
        return (result);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> AddRole(AddRoleCommand addRoleCommand)
    {
        var result =  await _mediator.Send(addRoleCommand);
        return Ok(result);
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteRole(RemoveRoleCommand removeRoleCommand)
    {

        if (await _mediator.Send(removeRoleCommand))
        {
            return Ok("Role deleted successfully");
        }
        else
        {
            return BadRequest("Role not found!!");
        }

    }
    

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommand updateRoleCommand)
    {
        var existRole = await _mediator.Send(new GetRoleByIdQuery()
        {
            Id = updateRoleCommand.Id
        });

        if (existRole==null)
        {
            return BadRequest($"No role found with the id {updateRoleCommand.Id}!!");
        }
        var result = await _mediator.Send(updateRoleCommand);

        return Ok(result);
    }
    
}