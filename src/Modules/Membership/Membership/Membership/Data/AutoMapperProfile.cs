

using AutoMapper;



namespace Membership.Data
{
    public class AutoMapperProfile : Profile
    {
        public  AutoMapperProfile() 
        {

            CreateMap<MembershipPlan, ViewMembershipPlanDto>()
                .ReverseMap();

        }
    }
}
