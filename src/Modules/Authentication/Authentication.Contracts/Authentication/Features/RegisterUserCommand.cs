using Authentication.Contracts.Authentication.Dtos;
using MediatR;


namespace Authentication.Contracts.Authentication.Features
{
    
    public record class RegisterUserCommand(RegisterUserDto userDto) : IRequest<RegisterUserCommandResponse>;

    public record RegisterUserCommandResponse(Guid UserId, string message);

}
