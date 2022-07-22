namespace UMS.Application.DTOs;

public class UserDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long RoleId { get; set; }
    public string Email { get; set; }
    public bool? Subscriber { get; set; }

}