using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Member.UpdateMember
{
    public record UpdateMemberCommand(Guid Id, UpdateMemberDto dto) : IRequest<Results>;
    public class UpdateMemberCommandHandler(MembershipDbContext _context) : IRequestHandler<UpdateMemberCommand, Results>
    {
        public async Task<Results> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _context.Members.FindAsync(request.Id);
            if (member == null)
            {
                return Results.Failure(MembershipModuleErrors.MembershipPlanErrors.NotFound(request.Id));
            }

            UpdateMember(member, request.dto);
            _context.Members.Update(member);
            await _context.SaveChangesAsync(cancellationToken);

            return Results.Success();
        }

        private void UpdateMember(Models.Member member, UpdateMemberDto dto)
        {
            member.Update(dto.FirstName, dto.LastName, dto.PhoneNumber, dto.Gender);
        }
    }
}
