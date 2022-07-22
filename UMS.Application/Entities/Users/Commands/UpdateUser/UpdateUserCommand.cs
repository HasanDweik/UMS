using MediatR;
using UMS.Application.DTOs;

namespace UMS.Application.Entities.Users.Commands.UpdateUser;

public class UpdateUserCommand:IRequest<UserDTO>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long RoleId { get; set; }
    public string Email { get; set; }
    public bool? Subscriber { get; set; }

}