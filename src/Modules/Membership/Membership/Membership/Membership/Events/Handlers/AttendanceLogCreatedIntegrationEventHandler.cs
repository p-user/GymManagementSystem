


using Membership.Membership.Features.Membership.UpdateVisitsRemaining;
using Microsoft.Extensions.Logging;

namespace Membership.Membership.Events.Handlers
{
    public class AttendanceLogCreatedIntegrationEventHandler(ILogger<AttendanceLogCreatedIntegrationEventHandler> _logger, ISender _sender) : IConsumer<AttendanceLogCreatedIntegrationEvent>
    {
        public async  Task Consume(ConsumeContext<AttendanceLogCreatedIntegrationEvent> context)
        {
            _logger.LogInformation($"Attendance log handled for user {context.Message.UserId}");
            await _sender.Send(new UpdateVisitsRemainingCommand(context.Message.UserId), CancellationToken.None);
        }
    }
}
