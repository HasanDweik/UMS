using _101SendEmailNotificationDoNetCoreWebAPI.Model;
using AutoMapper;
using MediatR;
using UMS.Application.DTOs;
using UMS.Domain.Models;
using UMS.Infrastructure.Abstraction.Services;

namespace UMS.Application.Entities.Users.Commands.AddUser;

public class AddUserHandler:IRequestHandler<AddUserCommand,UserDTO>
{
    private readonly UmsContext _context;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;

    public AddUserHandler(UmsContext context, IMapper mapper,IMailService mailService)
    {
        _mailService = mailService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User()
        {
            Name = request.Name,
            Email = request.Email,
            RoleId = request.RoleId,
            KeycloakId = request.KeycloakId,
            Subscriber = request.Subscriber
        };
        try
        {
            _context.Add(user);
            _context.SaveChanges();

            if (request.RoleId==3)
            {
                var subscribers =  _context.Users.Where(obj => obj.Subscriber == true).ToList();
                foreach (var VARIABLE in subscribers)
                {
                    Console.WriteLine("sending email to " + VARIABLE.Email);
                    _mailService.SendEmailAsync(new MailRequest()
                    {
                        ToEmail = VARIABLE.Email,
                        Subject = "New Teacher Available",
                        Body = "Hello Student," +
                               "\n" +
                               "Kindly note that a new teacher has been added."
                    });
                    Console.WriteLine("email sent to " + VARIABLE.Email);
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return _mapper.Map<UserDTO>(user);
    }
}