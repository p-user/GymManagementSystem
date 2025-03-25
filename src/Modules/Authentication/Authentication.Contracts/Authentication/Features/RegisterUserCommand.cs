using Authentication.Contracts.Authentication.Dtos;
using MediatR;
using Shared.Results;


namespace Authentication.Contracts.Authentication.Features
{

    public record RegisterUserCommand<T>(T userDto) : IRequest<Results<RegisterUserCommandResponseDto>> where T : RegisterUserDto;


}
