using MediatR;
using UMS.Domain.Models;

namespace UMS.Application.Entities.TeacherPerCoursePerSessionTime.Commands.AddTeacherPerCoursePerSessionTime;

public class AddTeacherPerCoursePerSessionTimeHandler:IRequestHandler<AddTeacherPerCoursePerSessionTimeCommand,Domain.Models.TeacherPerCoursePerSessionTime>
{

    private readonly UmsContext _context;

    public AddTeacherPerCoursePerSessionTimeHandler(UmsContext context)
    {
        _context = context;
    }

    public async Task<Domain.Models.TeacherPerCoursePerSessionTime> Handle(AddTeacherPerCoursePerSessionTimeCommand request, CancellationToken cancellationToken)
    {

        Domain.Models.TeacherPerCoursePerSessionTime teacherPerCoursePerSessionTime = new Domain.Models.TeacherPerCoursePerSessionTime()
        {
            TeacherPerCourseId = request.TeacherPerCourseId,
            SessionTimeId = request.SessionTimeId
        };

            _context.TeacherPerCoursePerSessionTimes.Add(teacherPerCoursePerSessionTime);
            _context.SaveChanges();
            return teacherPerCoursePerSessionTime;

    }
}