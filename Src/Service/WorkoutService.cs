using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class WorkoutService(DatabaseContext databaseContext, IMapper mapper) : BaseService<Workout>(databaseContext, mapper), IWorkoutService
{
    public async Task<List<WorkoutResponse>> GetAllWorkouts()
    {
        return await Mapper.ProjectTo<WorkoutResponse>(GetQueryable()).ToListAsync();
    }

    public async Task<WorkoutResponse> CreateWorkout(WorkoutRequest workoutRequest)
    {
        Workout newWorkout = Mapper.Map<WorkoutRequest, Workout>(workoutRequest);
        
        var workout = await CreateAsync(newWorkout);
        
        WorkoutResponse workoutResponse = Mapper.Map<Workout, WorkoutResponse>(workout);
        
        return workoutResponse;
    }

    public async Task<WorkoutDetailResponse> GetWorkoutById(int workoutId)
    {
        var workout = await GetQueryable().Include(w => w.Exercises).SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        WorkoutDetailResponse workoutDetailResponse = Mapper.Map<Workout, WorkoutDetailResponse>(workout);
        
        return workoutDetailResponse;
    }

    public async Task DeleteWorkoutById(int workoutId)
    {
        var workout = await GetQueryable().SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        await RemoveAsync(workout);
    }

    public async Task UpdateWorkoutById(int workoutId, WorkoutRequest workoutRequest)
    {
        var workout = await GetQueryable().SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }
        
        Mapper.Map(workoutRequest, workout);
        await SaveAsync();
    }
}