namespace Membership.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<MembershipPlan, ViewMembershipPlanDto>()
                .ReverseMap();

            CreateMap<Models.Membership, ViewMembershipDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.MembershipPlanName, opt => opt.MapFrom(src => src.MembershipPlan.Name))
           .ForMember(dest => dest.GymMemberName, opt => opt.MapFrom(src => $"{src.GymMember.FirstName} {src.GymMember.LastName}"))
           .ForMember(dest => dest.DiscountName, opt => opt.MapFrom(src => src.Discount != null ? src.Discount.Code : null))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<ViewMembershipDto, Models.Membership>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Models.Membership.MembershipStatus>(src.Status)));

            CreateMap<CreateMembershipDto, Models.Membership>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Models.Membership.MembershipStatus.Active))
            .ForMember(dest => dest.MembershipStartDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Models.Membership, MembershipDto>()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        }
    }
}
