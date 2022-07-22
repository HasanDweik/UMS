using AutoMapper;
using MediatR;
using UMS.Application.DTOs;
using UMS.Domain.Models;

namespace UMS.Application.Entities.Users.Queries.GetUsersByCourse;

public class GetUsersByCourseHandler:IRequestHandler<GetUsersByCourseQuery,List<UserDTO>>
{
    private readonly UmsContext _context;
    private readonly IMapper _mapper;

    public GetUsersByCourseHandler(UmsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDTO>> Handle(GetUsersByCourseQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Users
            .Join(_context.ClassEnrollments,
                u => u.Id,
                e => e.StudentId,
                (u, e) => new { u, e })
            .Join(_context.TeacherPerCourses,
                c => c.e.ClassId,
                t => t.Id,
                (c, t) => new { c, t })
            .Where(obj => obj.t.CourseId == request.Id);

        List<User> enrolledUsers = new List<User>();
        foreach (var VARIABLE in queryable)
        {
            enrolledUsers.Add(VARIABLE.c.u);
        }

        return _mapper.Map<List<UserDTO>>(enrolledUsers);

    }
}