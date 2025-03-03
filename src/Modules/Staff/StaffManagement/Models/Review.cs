
using Shared.DDD;

namespace StaffManagement.Models
{
    public class Review : Entity<Guid>
    {
        public ClientId MemberId { get; private set; }
        public int Rating { get; private set; } // 1-5 Stars
        public string Comment { get; private set; }
        public DateTime ReviewDate { get; private set; }
    }
}
