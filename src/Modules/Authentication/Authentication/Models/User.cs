using Microsoft.AspNetCore.Identity;

namespace Authentication.Models
{
    public class User : IdentityUser
    {
        
        public static User Create(string email, string name, string surname) => new User
        {
            AccessFailedCount = 0,
            Email = email,
            EmailConfirmed = false,
            Id = Guid.NewGuid().ToString(),
            LockoutEnabled = true,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper(),
            PasswordHash = "",
            PhoneNumber = "",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            UserName = email,
          


        };

       
    }

   
}
