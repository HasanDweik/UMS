using MediatR;

namespace UMS.Application.Entities.SessionTime.Commands.AddSessionTime;

public class AddSessionTimeCommand:IRequest<Domain.Models.SessionTime>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}