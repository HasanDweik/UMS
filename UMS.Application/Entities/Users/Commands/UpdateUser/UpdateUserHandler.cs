using AutoMapper;
using MediatR;
using UMS.Application.DTOs;
using UMS.Domain.Models;

namespace UMS.Application.Entities.Users.Commands.UpdateUser;

public class UpdateUserHandler:IRequestHandler<UpdateUserCommand,UserDTO>
{

    private readonly UmsContext _context;
    private readonly IMapper _mapper;

    public UpdateUserHandler(UmsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = _context.Users.Where(obj => obj.Id == request.Id).First();
        user.Name = request.Name;
        user.Email = request.Email;
        user.RoleId = request.RoleId;
        user.Subscriber = request.Subscriber;
        _context.Users.Update(user);
        _context.SaveChanges();
        return _mapper.Map<UserDTO>(user);
        
    }
}