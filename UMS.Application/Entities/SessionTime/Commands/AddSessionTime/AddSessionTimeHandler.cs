using AutoMapper;
using MediatR;
using UMS.Application.DTOs;
using UMS.Domain.Models;

namespace UMS.Application.Entities.SessionTime.Commands.AddSessionTime;

public class AddSessionTimeHandler:IRequestHandler<AddSessionTimeCommand,Domain.Models.SessionTime>
{

    private readonly UmsContext _context;
    private readonly IMapper _mapper;


    public AddSessionTimeHandler(UmsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Domain.Models.SessionTime> Handle(AddSessionTimeCommand request, CancellationToken cancellationToken)
    {
        Domain.Models.SessionTime sessionTime = new Domain.Models.SessionTime()
        {
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };
        try
        { 
            _context.Add(sessionTime);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return sessionTime;
    }
}