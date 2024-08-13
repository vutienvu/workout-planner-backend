using WorkoutPlanner.Entity;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;

namespace WorkoutPlanner.Helper;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Workout, WorkoutResponse>();
        CreateMap<Workout, WorkoutDetailResponse>();
        
        CreateMap<WorkoutRequest, Workout>();

        CreateMap<Exercise, ExerciseResponse>();
        CreateMap<Exercise, ExerciseDetailResponse>();
        
        CreateMap<ExerciseRequest, Exercise>();

        CreateMap<ExerciseTerm, ExerciseTermResponse>();
        CreateMap<ExerciseTermRequest, ExerciseTerm>();

        CreateMap<Set, SetResponse>();
        CreateMap<SetRequest, Set>();
    }
}