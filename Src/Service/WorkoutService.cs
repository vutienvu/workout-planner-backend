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
        var workouts = await databaseContext.Workouts.Include(w => w.Exercises).ToListAsync();
        
        List<WorkoutResponse> workoutsDto = mapper.Map<List<Workout>, List<WorkoutResponse>>(workouts);
        
        return workoutsDto;
    }

    public async Task<WorkoutResponse> CreateWorkout(WorkoutRequest workoutRequest)
    {
        Workout newWorkout = mapper.Map<WorkoutRequest, Workout>(workoutRequest);
        
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

    public async Task UpdateWorkoutById(int workoutId, WorkoutRequest workoutRequest)
    {
        var workout = await databaseContext.Workouts.SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }
        
        mapper.Map(workoutRequest, workout);
        await databaseContext.SaveChangesAsync();
    }
}