namespace Authentication.Models
{
    public class User : IdentityUser
    {

        public static User Create(string email, string name, string surname, string phonenumber) => new User
        {
            AccessFailedCount = 0,
            Email = email,
            EmailConfirmed = false,
            Id = Guid.NewGuid().ToString(),
            LockoutEnabled = true,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper(),
            PasswordHash = "",
            PhoneNumber = phonenumber,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            UserName = email,



        };


    }


}
