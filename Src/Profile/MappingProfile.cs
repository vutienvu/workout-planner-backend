using WorkoutPlanner.Entity;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;

namespace WorkoutPlanner.Profile;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Workout, WorkoutResponse>();
        CreateMap<WorkoutRequest, Workout>();
    }
}