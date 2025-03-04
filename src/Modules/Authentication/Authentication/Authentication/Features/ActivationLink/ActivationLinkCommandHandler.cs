
using Authentication.Models;
using Authentication.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Authentication.Features.ActivationLink
{
    public record ActivationLinkCommand(User User) : IRequest<bool>;
    public record ActivationLinkResponse();


    public class ActivationLinkCommandHandler(IEmailService emailService, UserManager<User> _userManager) : IRequestHandler<ActivationLinkCommand, bool>
    {
        public async Task<bool> Handle(ActivationLinkCommand request, CancellationToken cancellationToken)
        {
             var token = await _userManager.GenerateEmailConfirmationTokenAsync(request.User);
            var message = $"Please click on the link to activate your account: https://localhost:5055/scalar/v1/set-password?userId={request.User.Id}&token={Uri.EscapeDataString(token)}";
           await emailService.SendEmailAsync(request.User.Email, "GYM.Api Account Activation", message);
           return true;
        }
    }

}
