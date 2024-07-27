using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class WorkoutService(DatabaseContext databaseContext, IMapper mapper) : IWorkoutService
{
    public async Task<List<WorkoutResponse>> GetAllWorkouts()
    {
        var workouts = await databaseContext.Workouts.ToListAsync();

        var workoutsDto = new List<WorkoutResponse>();

        foreach (var workout in workouts)
        {
            WorkoutResponse workoutResponse = mapper.Map<Workout, WorkoutResponse>(workout);
            
            workoutsDto.Add(workoutResponse);
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
        
        WorkoutResponse workoutResponse = mapper.Map<Workout, WorkoutResponse>(workout);
        
        return workoutResponse;
    }

    public async Task<WorkoutResponse> GetWorkoutById(int workoutId)
    {
        var workout = await databaseContext.Workouts.SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }
        
        WorkoutResponse workoutResponse = mapper.Map<Workout, WorkoutResponse>(workout);
        
        return workoutResponse;
    }

    public async Task DeleteWorkoutById(int workoutId)
    {
        var workout = await databaseContext.Workouts.SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        databaseContext.Workouts.Remove(workout);
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateWorkoutById(int workoutId, string newName)
    {
        var workout = await databaseContext.Workouts.SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        workout.Name = newName;
        await databaseContext.SaveChangesAsync();
    }
}