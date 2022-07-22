using System.ComponentModel.DataAnnotations;
using MediatR;
using NpgsqlTypes;
using UMS.Domain.Models;

namespace UMS.Application.Entities.Courses.Commands.AddCourse;

public class AddCourseCommand:IRequest<Course>
{
    [Required]
    public string Name { get; set; }
    public int MaxStudentsNumber { get; set; }
    
    public DateTime EnrolmentStart { get; set; } 
    public DateTime EnrolmentEnd { get; set; } 
    
}