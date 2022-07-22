using MediatR;
using UMS.Application.DTOs;

namespace UMS.Application.Entities.Users.Queries.GetUsersByCourse;

public class GetUsersByCourseQuery:IRequest<List<UserDTO>>
{
    public long Id { get; set; }
}