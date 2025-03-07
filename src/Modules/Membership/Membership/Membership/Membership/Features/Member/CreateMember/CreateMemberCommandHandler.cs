
namespace Membership.Membership.Features.Member.CreateMember
{
    public record  CreateMemberCommand(CreateMemberDto dto) : IRequest<CreateMemberResponse>;
    public record CreateMemberResponse(string Message);

    public class CreateMemberCommandHandler(ISender _sender, MembershipDbContext _context) 
        : IRequestHandler<CreateMemberCommand, CreateMemberResponse>
    {
        public async Task<CreateMemberResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            //Todo : validate dto

            var response = await _sender.Send(new RegisterUserCommand<CreateMemberDto>(request.dto));
            if (response.UserId == Guid.Empty) { throw new Exception("AuthenticationId was not provided! Something went wrong"); }

            //register the user as a valid gym memeber
            var member = CreateMember(request.dto, response.UserId);
            var added = await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateMemberResponse("Member created successfully, check your email for activation link!");
        }

        private GymMember CreateMember(CreateMemberDto dto, Guid userId)
        {
            return GymMember.Create(
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
