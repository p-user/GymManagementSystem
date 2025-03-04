

namespace Authentication.Authentication.Dtos
{
    public class SetPasswordDto
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
