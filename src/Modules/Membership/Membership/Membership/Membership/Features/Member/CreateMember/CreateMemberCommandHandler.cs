
namespace Membership.Membership.Features.Member.CreateMember
{
    public record CreateMemberCommand(CreateMemberDto dto) : IRequest<Results<string>>;

    public class CreateMemberCommandHandler(ISender _sender, MembershipDbContext _context)
        : IRequestHandler<CreateMemberCommand, Results<string>>
    {
        public async Task<Results<string>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            //Todo : validate dto

            var response = await _sender.Send(new RegisterUserCommand<CreateMemberDto>(request.dto));
            if (response.Value.UserId == Guid.Empty) { throw new Exception("AuthenticationId was not provided! Something went wrong"); }

            //register the user as a valid gym memeber
            var member = CreateMember(request.dto, response.Value.UserId);
            var added = await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync(cancellationToken);

            return ("Member created successfully, check your email for activation link!");
        }

        private Models.Member CreateMember(CreateMemberDto dto, Guid userId)
        {
            return Models.Member.Create(
                    userId,
                    dto.Name,
                    dto.Surname,
                    dto.Email,
                    dto.Telephone,
                    dto.Gender
                );
        }
    }

}
