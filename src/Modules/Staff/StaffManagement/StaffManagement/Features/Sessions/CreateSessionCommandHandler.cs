using Results = Shared.Results.Results;

namespace StaffManagement.StaffManagement.Features.Sessions
{
    public record  CreateSessionCommand(CreateSessionDto dto) : IRequest<Results>;
    public class CreateSessionCommandHandler(ISender _sender, StaffDbContext _context) : IRequestHandler<CreateSessionCommand, Results>
    {
        public async  Task<Results> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            //
           var member = await _sender.Send(new GetMemberByIdQuery(request.dto.MemberId));
            if (member is null)
            {
                return (Results<string>)Results.Failure(Membership.Contracts.Membership.ModuleErrors.MemberErrors.NotFound(request.dto.MemberId));
            }
            var session = CreateSession(request.dto);
            var added = await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Success();

        }

        private PrivateSession CreateSession(CreateSessionDto dto)
        {
           return PrivateSession.Create(dto.TrainerId, dto.MemberId, dto.ScheduledAt);
        }
    }
}
