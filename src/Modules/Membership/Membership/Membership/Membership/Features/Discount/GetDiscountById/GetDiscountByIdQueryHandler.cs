
using Results = Shared.Results.Results;
namespace Membership.Membership.Features.Discount.GetDiscountById
{

    public record GetDiscountByIdQuery(Guid id) : IRequest<Results<DiscountDto>>;
    public class GetDiscountByIdQueryHandler(MembershipDbContext _context, IMapper _mapper) : IRequestHandler<GetDiscountByIdQuery, Results<DiscountDto>>
    {
        public async Task<Results<DiscountDto>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.FindAsync(request.id);
            if (discount == null)
            {
                return Results.Failure<DiscountDto>(MembershipModuleErrors.DicountErrors.NotFound(request.id.ToString()));
            }

            var result = _mapper.Map<DiscountDto>(discount);
            return result;


        }
    }
}
