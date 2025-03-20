using MediatR;
using Shared.Results;


namespace Membership.Contracts.Membership.Features.Member
{
    public record CheckForValidMembershipQuery(Guid MemberId) : IRequest<Results<bool>>;
}
