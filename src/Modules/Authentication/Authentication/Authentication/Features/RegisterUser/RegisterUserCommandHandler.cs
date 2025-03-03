
using Authentication.Contracts.Authentication.Dtos;
using Authentication.Contracts.Authentication.Features;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Authentication.Features.RegisterUser
{


    public class RegisterUserCommandHandler(UserManager<Models.User> _userManager, RoleManager<Models.Role> _roleManager, ISender sender) : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var roleEntity = await _roleManager.FindByNameAsync(request.userDto.UserRole);
            if (roleEntity is null)
            {
                throw new Exception("The role you provided is not valid!");
            }

            var appUser = CreateUser(request.userDto.Email, request.userDto.Name, request.userDto.Surname, request.userDto.Telephone);

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

          
            await _userManager.AddToRoleAsync(appUser, request.userDto.UserRole);

         //Todo: create activation linik and send via email

            var userId = new Guid(appUser.Id);
            return new RegisterUserCommandResponse(userId,"User registration is almost complete. Please, check the email address for the activation link!");

        }

        private Models.User CreateUser(string email, string name, string surname, string phonenumber)
        {
            return Models.User.Create(email, name, surname, phonenumber);
        }
    }
       
    
}
