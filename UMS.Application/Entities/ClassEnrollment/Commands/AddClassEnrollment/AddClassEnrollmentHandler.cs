using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Domain.Models;

namespace UMS.Application.Entities.ClassEnrollment.Commands.AddClassEnrollment;

public class AddClassEnrollmentHandler:IRequestHandler<AddClassEnrollmentCommand,Domain.Models.ClassEnrollment>
{

    private readonly UmsContext _context;


    public AddClassEnrollmentHandler(UmsContext context)
    {
        _context = context;
    }

    public async Task<Domain.Models.ClassEnrollment> Handle(AddClassEnrollmentCommand request, CancellationToken cancellationToken)
    {

        Domain.Models.ClassEnrollment classEnrollment = new Domain.Models.ClassEnrollment()
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId
        };

            _context.ClassEnrollments.Add(classEnrollment);
            _context.SaveChanges();
            return classEnrollment;

        return null;
    }
}