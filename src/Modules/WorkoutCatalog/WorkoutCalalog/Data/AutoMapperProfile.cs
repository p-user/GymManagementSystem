namespace WorkoutCalalog.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Exercise, CreateExerciseDto>()
                 .ForMember(dest => dest.ExerciseCategory, opt => opt.MapFrom(src => src.ExerciseCategory))
                 .ReverseMap();

            CreateMap<Exercise, ViewExerciseDto>()
                 .ForMember(dest => dest.MuscleGroups, opt => opt.MapFrom(src => src.MuscleGroups))
                .ReverseMap();

            CreateMap<Exercise, UpdateExerciseDto>()

               .ReverseMap();

            CreateMap<WorkoutCategory, ViewWorkoutCategoryDto>()
              .ReverseMap();

            CreateMap<WorkoutCategory, CreateWorkoutCategoryDto>()
             .ReverseMap();
            CreateMap<WorkoutCategory, UpdateWorkoutCategoryDto>()
             .ReverseMap();

            CreateMap<MuscleGroup, ViewMuscleGroupDto>()
             // .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises))
             .ReverseMap();

        }
    }
}
