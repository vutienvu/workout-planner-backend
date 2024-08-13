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
        return Mapper.Map<WorkoutResponse>(await CreateAsync(Mapper.Map<Workout>(workoutRequest)));
    }

    public async Task<WorkoutDetailResponse> GetWorkoutById(int workoutId)
    {
        var workout = await Mapper.ProjectTo<WorkoutDetailResponse>(GetQueryable().Where(w => w.WorkoutId == workoutId)).SingleOrDefaultAsync();

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        return workout;
    }

    public async Task DeleteWorkoutById(int workoutId)
    {
        await RemoveAsync(await FindDbAsync(workoutId));
    }

    public async Task UpdateWorkoutById(int workoutId, WorkoutRequest workoutRequest)
    {
        Mapper.Map(workoutRequest, await FindDbAsync(workoutId));
        await SaveAsync();
    }

    private async Task<Workout> FindDbAsync(int workoutId)
    {
        var workout = await GetQueryable().SingleOrDefaultAsync(w => w.WorkoutId == workoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        return workout;
    }
}