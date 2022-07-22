using MediatR;

namespace UMS.Application.Entities.TeacherPerCoursePerSessionTime.Commands.AddTeacherPerCoursePerSessionTime;

public class AddTeacherPerCoursePerSessionTimeCommand : IRequest<Domain.Models.TeacherPerCoursePerSessionTime>
{
    public long TeacherPerCourseId { get; set; }
    public long SessionTimeId { get; set; }
}