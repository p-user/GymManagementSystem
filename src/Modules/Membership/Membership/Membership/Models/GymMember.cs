
using Shared.DDD;
using Shared.Enums;

namespace Membership.Models
{
    public class GymMember : Entity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public Gender Gender { get; private set; }
        public bool HasActiveMembership { get; private set; }

        public Guid AuthenticationId { get; private set; } //from authentication module


        public static GymMember Create(Guid authenticationId, string firstName, string lastName, string email, string phoneNumber,  Gender gender)
        {
            ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));
            ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));
            ArgumentNullException.ThrowIfNull(email, nameof(email));
            ArgumentNullException.ThrowIfNull(phoneNumber, nameof(phoneNumber));

            var member = new GymMember
            {
                Id = Guid.NewGuid(),
                AuthenticationId = authenticationId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                HasActiveMembership = false,
                Gender= gender

            };

            return member;
        }
    }
}
