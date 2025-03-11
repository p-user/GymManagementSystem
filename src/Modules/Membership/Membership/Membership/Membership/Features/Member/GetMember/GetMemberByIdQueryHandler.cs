
using AutoMapper;
using Membership.Membership.Dtos;

namespace Membership.Membership.Features.Member.GetMember
{
    public record GetMemberByIdQuery(Guid Id) : IRequest<GetMemberByIdResponse>;
    public record GetMemberByIdResponse(MemberDto Member);

    public class GetMemberByIdQueryHandler(MembershipDbContext _membershipDbContext, IMapper _mapper) : IRequestHandler<GetMemberByIdQuery, GetMemberByIdResponse>
    {
        public async Task<GetMemberByIdResponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _membershipDbContext.Members.FindAsync(request.Id);
            if (entity is null)
            {
                throw new Exception("Member not found!");
            }
            var dto = _mapper.Map<MemberDto>(entity);
            return new GetMemberByIdResponse(dto);  
        }
    }
}
