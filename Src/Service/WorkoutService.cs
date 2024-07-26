using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
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

    public async Task<WorkoutResponse> CreateWorkout(WorkoutRequest workoutRequest)
    {
        var newWorkout = new Workout();

        newWorkout.Name = workoutRequest.Name;
        
        var workoutEntity = await databaseContext.Workouts.AddAsync(newWorkout);
        await databaseContext.SaveChangesAsync();

        var workout = workoutEntity.Entity;
        
        var workoutResponse = new WorkoutResponse();

        workoutResponse.WorkoutId = workout.WorkoutId;
        workoutResponse.Name = workout.Name;

        return workoutResponse;
    }
}