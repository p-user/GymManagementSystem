

namespace Authentication.Authentication.Dtos
{
    public record RequestTokenResponseDto
    {
        public string AccessToken { get; init; }
        public string TokenType { get; init; }
        public string RefreshToken { get; init; }
        public int ExpiresIn { get; init; }
    }
}
