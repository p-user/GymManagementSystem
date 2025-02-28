
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Authentication.Authentication.Features.RegisterUser
{

    public record class RegisterUserCommand(string Email, string name, string surname): IRequest<RegisterUserCommandResponse>;
    public record RegisterUserCommandResponse(string test);

    public class RegisterUserCommandHandler(UserManager<Models.User> _userManager, ISender sender) : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var appUser = CreateUser(request.Email, request.name, request.surname);

            //check if user exists
            var existingUser = await _userManager.FindByEmailAsync(appUser.Email);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            //create user
            var result = await _userManager.CreateAsync(appUser);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }

            //Todo : assign to role
            //await _userManager.AddToRoleAsync(appUser, role);

            //create otp and send email
            //await sender.Send(new GenerateOTPCommand(appUser.Id));

            return new RegisterUserCommandResponse("true");

        }

        private Models.User CreateUser(string email, string name, string surname)
        {
            return Models.User.Create(email, name, surname);
        }
    }
       
    
}
