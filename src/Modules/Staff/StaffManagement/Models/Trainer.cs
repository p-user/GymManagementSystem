
using Shared.DDD;
using Shared.Enums;

namespace StaffManagement.Models
{
    public class Trainer : Aggregation<Guid>
    {
        public Guid AuthenticationId { get; private set; } //from authentication module
        public FullName Name { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public ContactInfo Contact { get; private set; }
        public EmploymentType EmploymentType { get; private set; }
        public Money? HourlyRate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime HireDate { get; private set; }



        private readonly List<Specialization> _specializations = new();
        public IReadOnlyCollection<Specialization> Specializations => _specializations.AsReadOnly();

        private readonly List<Certification> _certifications = new();
        public IReadOnlyCollection<Certification> Certifications => _certifications.AsReadOnly();

        private readonly List<ClientId> _currentClients = new();
        public IReadOnlyCollection<ClientId> CurrentClients => _currentClients.AsReadOnly();

        private readonly List<Review> _reviews = new();

        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        private readonly List<PrivateSession> _sessions = new();

        public IReadOnlyCollection<PrivateSession> Sessions => _sessions.AsReadOnly();






        public static Trainer Create(Guid authenticationId,
                                        FullName name,
                                        DateTime? dateOfBirth,
                                        Gender gender,
                                        ContactInfo contact,
                                        EmploymentType employmentType,
                                        Money? hourlyRate,
                                        DateTime hireDate,
                                        List<Specialization>? specializations,
                                        List<Certification>? certifications
                                        )
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(contact, nameof(contact));

            var trainer = new Trainer
            {
                Id = Guid.NewGuid(),
                AuthenticationId = authenticationId,
                Name = name,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Contact = contact,
                EmploymentType = employmentType,
                HourlyRate = hourlyRate,
                HireDate = hireDate,
                IsActive = true,
            };

            if (specializations?.Any() == true)
            {
                trainer._specializations.AddRange(specializations);
            }

            if (certifications?.Any() == true)
            {
                trainer._certifications.AddRange(certifications);
            }

            return trainer;
        }


        public PrivateSession ScheduleSession(Guid memberId, DateTime scheduledAt)
        {
            var session = PrivateSession.Create(Id, memberId, scheduledAt);
            _sessions.Add(session);
            return session;
        }


    }
}
