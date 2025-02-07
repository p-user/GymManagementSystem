

using AutoMapper;


namespace WorkoutCalalog.Data
{
    public class AutoMapperProfile : Profile
    {
        public  AutoMapperProfile() 
        {

            CreateMap<Exercise, CreateExerciseDto>()
                 .ReverseMap();

            CreateMap<Exercise, ViewExerciseDto>()
                .ReverseMap();
        }
    }
}
