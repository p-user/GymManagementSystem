
using AutoMapper;
using Membership.Membership.Dtos;
using Membership.Membership.ModuleErrors;
using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Member.GetMember
{
    public record GetMemberByIdQuery(Guid Id) : IRequest<Results<MemberDto>>;
    public record GetMemberByIdResponse(MemberDto Member);

    public class GetMemberByIdQueryHandler(MembershipDbContext _membershipDbContext, IMapper _mapper) : IRequestHandler<GetMemberByIdQuery, Results<MemberDto>>
    {
        public async Task<Results<MemberDto>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _membershipDbContext.Members.FindAsync(request.Id);
            if (entity is null)
            {
                return (Results<MemberDto>)Results.Failure(MemberErrors.NotFound(request.Id));
            }
            var dto = _mapper.Map<MemberDto>(entity);
            return dto;  
        }
    }
}
