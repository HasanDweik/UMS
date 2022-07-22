using _101SendEmailNotificationDoNetCoreWebAPI.Model;
using MediatR;
using NpgsqlTypes;
using UMS.Application.Services;
using UMS.Domain.Models;
using UMS.Infrastructure.Abstraction.Services;

namespace UMS.Application.Entities.Courses.Commands.AddCourse;

public class AddCourseHandler:IRequestHandler<AddCourseCommand,Course>
{
    private readonly UmsContext _context;
    private readonly IMailService _mailService;

    public AddCourseHandler(UmsContext context, IMailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    public async Task<Course> Handle(AddCourseCommand request, CancellationToken cancellationToken)
    {
        Course course = new Course()
        {
            Name = request.Name,
            MaxStudentsNumber = request.MaxStudentsNumber,
            EnrolmentDateRange = new NpgsqlRange<DateOnly>(
                new DateOnly(request.EnrolmentStart.Year,request.EnrolmentStart.Month,request.EnrolmentStart.Day),
                new DateOnly(request.EnrolmentEnd.Year,request.EnrolmentEnd.Month,request.EnrolmentEnd.Day))
        };
        try
        { 
            _context.Courses.Add(course);
            _context.SaveChanges();
            
            //sending notification to students

            var subscribers =  _context.Users.Where(obj => obj.Subscriber == true & obj.RoleId==3).ToList();
            foreach (var VARIABLE in subscribers)
            {
                Console.WriteLine("sending email to "+VARIABLE.Email);
                _mailService.SendEmailAsync(new MailRequest()
                {
                    ToEmail = VARIABLE.Email,
                    Subject = "New Course Available",
                    Body = $"Hello {VARIABLE.Name}," +
                           "\n" +
                           "Kindly note that a new course has been added."
                });
                Console.WriteLine("email sent to "+VARIABLE.Email);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return course;
    }
}