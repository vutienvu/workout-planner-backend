using WorkoutPlanner.Entity;
using WorkoutPlanner.Response;

namespace WorkoutPlanner.Profile;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Workout, WorkoutResponse>();
    }
}