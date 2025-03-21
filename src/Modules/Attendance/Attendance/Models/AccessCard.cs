

namespace Attendance.Models
{
    public class AccessCard : Entity<Guid>
    {
        public string CardNumber { get; set; }
        public Guid OwnerId { get; private set; } //Todo : decide memberid and staffid or authenticationid
        public AccessCardOwnerType OwnerType { get; private set; }
        public AccessCardStatus Status { get; private set; }
        public DateTime IssuedAt { get; private set; }

        private AccessCard() { }

        public static AccessCard Create(Guid userId,AccessCardOwnerType ownerType)
        {
            return new AccessCard
            {

                Id = Guid.NewGuid(),
                OwnerId = userId,
                OwnerType = ownerType,
                CardNumber = GenerateCardNumber(),
                Status = AccessCardStatus.Active,
                IssuedAt = DateTime.UtcNow,
            };
         }

        public enum AccessCardStatus
        {
            Active,
            Inactive,
            Lost,
        }

        public enum AccessCardOwnerType
        {
            Member,
            Staff
        }

        private static string GenerateCardNumber()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }


        public void Deactivate() => Status = AccessCardStatus.Inactive;

        public void MarkAsLost() => Status = AccessCardStatus.Lost;

        
    }
}
