

namespace Authentication.Authentication.Features.SetPassword
{
    public record SetPasswordCommand(SetPasswordDto dto) : IRequest<SetPasswordCommandResponse>;
    public record SetPasswordCommandResponse(string message);
    public class SetPasswordCommandHandler(UserManager<Models.User> _userManager) : IRequestHandler<SetPasswordCommand, SetPasswordCommandResponse>
    {
        public async Task<SetPasswordCommandResponse> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.dto.UserId.ToString());
            if (user == null)
                throw new Exception("Invalid user");

            // Verify the token
            var result = await _userManager.ResetPasswordAsync(user, request.dto.Token, request.dto.Password);
            if (!result.Succeeded)
                throw new Exception("Invalid or expired token");

            user.EmailConfirmed = true;  // Account is now activated
            await _userManager.UpdateAsync(user);

            return new SetPasswordCommandResponse("Account activated and password set successfully!");
        }
    }
   
}
