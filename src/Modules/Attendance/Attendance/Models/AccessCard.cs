

namespace Attendance.Models
{
    public class AccessCard : Entity<Guid>
    {
        public string CardNumber { get; set; }
        public Guid UserId { get; private set; } //Todo : decide memberid and staffid or authenticationid
        public AccessCardStatus Status { get; private set; }
        public DateTime IssuedAt { get; private set; }

        private AccessCard() { }

        public static AccessCard Create(Guid userId, string cardNumber)
        {
            return new AccessCard
            {

                Id = Guid.NewGuid(),
                UserId = userId,
                CardNumber = cardNumber,
                Status = AccessCardStatus.Active,
                IssuedAt = DateTime.UtcNow,
            };
         }

        public enum AccessCardStatus
        {
            Active,
            Inactive,
            Lost,
            Expired
        }


        public void Deactivate() => Status = AccessCardStatus.Inactive;

        public void MarkAsLost() => Status = AccessCardStatus.Lost;

        
    }
}
