using MediatR;

namespace UMS.Application.Entities.ClassEnrollment.Commands.AddClassEnrollment;

public class AddClassEnrollmentCommand : IRequest<Domain.Models.ClassEnrollment>
{
    public long ClassId { get; set; }
    public long StudentId { get; set; }
}