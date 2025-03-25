

namespace Authentication.Authentication.Dtos
{
    public record RefreshTokenResponseDto(string AccessToken, string TokenType, string RefreshToken, int ExpiresIn);

}
