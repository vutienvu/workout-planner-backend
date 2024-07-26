using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class WorkoutService(DatabaseContext databaseContext) : IWorkoutService
{
    public async Task<List<WorkoutResponse>> GetAllWorkouts()
    {
        var workouts = await databaseContext.Workouts.ToListAsync();

        var workoutsDto = new List<WorkoutResponse>();

        foreach (var workout in workouts)
        {
            var workoutDto = new WorkoutResponse();

            workoutDto.WorkoutId = workout.WorkoutId;
            workoutDto.Name = workout.Name;
            
            workoutsDto.Add(workoutDto);
        }
        
        return workoutsDto;
    }
}