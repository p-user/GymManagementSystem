

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

            CreateMap<Exercise, UpdateExerciseDto>()
               .ReverseMap();

            CreateMap<WorkoutCategory, ViewWorkoutCategoryDto>()
              .ReverseMap();

            CreateMap<WorkoutCategory, CreateWorkoutCategoryDto>()
             .ReverseMap();
            CreateMap<WorkoutCategory, UpdateWorkoutCategoryDto>()
             .ReverseMap();

            
        }
    }
}
