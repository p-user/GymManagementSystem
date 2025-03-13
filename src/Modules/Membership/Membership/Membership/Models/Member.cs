
using static Membership.Models.Membership;

namespace Membership.Models
{
    public class Member : Entity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public Gender Gender { get; private set; }
        public bool HasActiveMembership => _memberships.Exists(m => m.Status == MembershipStatus.Active);

        private List<Membership> _memberships = new List<Membership>();

        public IReadOnlyList<Membership> Memberships => _memberships.AsReadOnly();

        public Guid AuthenticationId { get; private set; } //from authentication module


        public static Member Create(Guid authenticationId, string firstName, string lastName, string email, string phoneNumber,  Gender gender)
        {
            ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));
            ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));
            ArgumentNullException.ThrowIfNull(email, nameof(email));
            ArgumentNullException.ThrowIfNull(phoneNumber, nameof(phoneNumber));

            var member = new Member
            {
                Id = Guid.NewGuid(),
                AuthenticationId = authenticationId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Gender= gender

            };

            return member;
        }


        public void Update(string firstName, string lastName, string phoneNumber, Gender gender)
        {
            ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));
            ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));
            ArgumentNullException.ThrowIfNull(phoneNumber, nameof(phoneNumber));


            FirstName = firstName;
            LastName = lastName;

            PhoneNumber = phoneNumber;
            Gender = gender;
        }

        private Member() { }

        public void AddMembership(Membership membership)
        {
            _memberships.Add(membership);
        }

        public void RemoveMembership(Membership membership)
        {
            _memberships.Remove(membership);
        }
    }
}
