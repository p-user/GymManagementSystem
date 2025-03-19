using MediatR;
using Membership.Contracts.Membership.Dtos;
using Shared.Results;


namespace Membership.Contracts.Membership.Features.Member
{

    public record GetMemberByIdQuery(Guid Id) : IRequest<Results<MemberDto>>;
}
