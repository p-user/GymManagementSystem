namespace Membership.Membership.Features.Member.UpdateMember
{
    public record UpdateMemberCommand(Guid Id, UpdateMemberDto dto) : IRequest<UpdateMemberResponse>;
    public record UpdateMemberResponse(Guid id);
    public class UpdateMemberCommandHandler(MembershipDbContext _context) : IRequestHandler<UpdateMemberCommand, UpdateMemberResponse>
    {
        public async Task<UpdateMemberResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _context.Members.FindAsync(request.Id);
            if (member == null)
            {
                throw new Exception("Member was not found!");
            }

            UpdateMember(member, request.dto);
            _context.Members.Update(member);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateMemberResponse(member.Id);
        }

        private void UpdateMember(Models.Member member, UpdateMemberDto dto)
        {
            member.Update(dto.FirstName, dto.LastName, dto.PhoneNumber, dto.Gender);
        }
    }
}
