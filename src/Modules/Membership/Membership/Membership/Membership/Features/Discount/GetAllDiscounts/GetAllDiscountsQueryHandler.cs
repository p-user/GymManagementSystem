

using AutoMapper;
using Results = Shared.Results.Results;

namespace Membership.Membership.Features.Discount.GetAllDiscounts
{

    public record GetAllDiscountsQuery : IRequest<Results<List<DiscountDto>>>;

    public class GetAllDiscountsQueryHandler(MembershipDbContext _context, IMapper _mapper) : IRequestHandler<GetAllDiscountsQuery, Results<List<DiscountDto>>>
    {
        public async Task<Results<List<DiscountDto>>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            var discounts = await _context.Discounts.ToListAsync(cancellationToken);
             var result = _mapper.Map<List<DiscountDto>>(discounts);
            return result;

        }
    }
}
