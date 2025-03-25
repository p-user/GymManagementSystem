namespace Attendance.Attendance.Events.Handlers
{
    public class AttendanceLogCreatedEventHandler(ILogger<AttendanceLogCreatedEventHandler> _logger, IBus _bus) : INotificationHandler<AttendanceLogCreatedEvent>
    {
        public async Task Handle(AttendanceLogCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attendance log created for user {notification.GetType()}");
            var integrationEvent = new AttendanceLogCreatedIntegrationEvent() { UserId = notification.UserId };
            await _bus.Publish(integrationEvent, cancellationToken);
        }
    }
}
