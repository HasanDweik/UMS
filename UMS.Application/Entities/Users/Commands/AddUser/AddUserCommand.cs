using MediatR;
using UMS.Application.DTOs;
using UMS.Domain.Models;

namespace UMS.Application.Entities.Users.Commands.AddUser;

public class AddUserCommand:IRequest<UserDTO>
{
    public string Name { get; set; }
    public string Email {get; set; }
    public long RoleId {get; set; }
    public string KeycloakId { get; set; }
    public bool? Subscriber { get; set; }

    
}