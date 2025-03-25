

namespace Authentication.Authentication.Features.SetPassword
{
    public record SetPasswordCommand(SetPasswordDto dto) : IRequest<Results<string>>;
    public record SetPasswordCommandResponse(string message);
    public class SetPasswordCommandHandler(UserManager<Models.User> _userManager) : IRequestHandler<SetPasswordCommand, Results<string>>
    {
        public async Task<Results<string>> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.dto.UserId.ToString());
            if (user == null)
            {
                throw new Exception("Invalid user");

            }

            // Verify the token
            var decodedToken = WebUtility.UrlDecode(request.dto.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.dto.Password);
            if (!result.Succeeded)
                throw new Exception("Invalid or expired token");

            user.EmailConfirmed = true;  // Account is now activated
            await _userManager.UpdateAsync(user);

            return new string("Account activated and password set successfully!");
        }
    }

}
