using Authentication.Contracts.Authentication.Dtos;
using MediatR;


namespace Authentication.Contracts.Authentication.Features
{
    
    public record RegisterUserCommand<T>(T userDto) : IRequest<RegisterUserCommandResponse> where T : RegisterUserDto;

    public record RegisterUserCommandResponse(Guid UserId, string message);

}
