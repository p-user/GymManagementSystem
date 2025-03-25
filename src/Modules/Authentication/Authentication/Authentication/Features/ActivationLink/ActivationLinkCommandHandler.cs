namespace Authentication.Authentication.Features.ActivationLink
{
    public record ActivationLinkCommand(User User) : IRequest<Shared.Results.Results>;

    public class ActivationLinkCommandHandler(IEmailService emailService, UserManager<User> _userManager) : IRequestHandler<ActivationLinkCommand, Shared.Results.Results>
    {
        public async Task<Shared.Results.Results> Handle(ActivationLinkCommand request, CancellationToken cancellationToken)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(request.User);
            var encodedToken = WebUtility.UrlEncode(token);
            var message = $"Please click on the link to activate your account: https://localhost:5055/scalar/v1/set-password?userId={request.User.Id}&token={encodedToken}";
            await emailService.SendEmailAsync(request.User.Email, "GYM.Api Account Activation", message);
            return Shared.Results.Results.Success();
        }
    }

}
