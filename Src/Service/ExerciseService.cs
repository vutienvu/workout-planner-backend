using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class ExerciseService(DatabaseContext databaseContext, IMapper mapper) : BaseService<Exercise>(databaseContext, mapper), IExerciseService
{
    public async Task<List<ExerciseResponse>> GetAllExercises()
    {
        return await Mapper.ProjectTo<ExerciseResponse>(GetQueryable()).ToListAsync();
    }

    public async Task<ExerciseResponse> CreateExercise(ExerciseRequest exerciseRequest)
    {
        Exercise newExercise = Mapper.Map<ExerciseRequest, Exercise>(exerciseRequest);

        var exercise = await CreateAsync(newExercise);
        
        ExerciseResponse exerciseResponse = Mapper.Map<Exercise, ExerciseResponse>(exercise);

        return exerciseResponse;
    }

    public async Task<ExerciseDetailResponse> GetExerciseById(int exerciseId)
    {
        var exercise = await GetQueryable().Include(e => e.ExerciseTerms).SingleOrDefaultAsync(e => e.ExerciseId == exerciseId);

        if (exercise == null)
        {
            throw new Exception("No exercise with such id.");
        }

        ExerciseDetailResponse exerciseResponse = Mapper.Map<Exercise, ExerciseDetailResponse>(exercise);

        return exerciseResponse;
    }

    public async Task DeleteExerciseById(int exerciseId)
    {
        var exercise = await GetQueryable().SingleOrDefaultAsync(e => e.ExerciseId == exerciseId);

        if (exercise == null)
        {
            throw new Exception("No exercise with such id.");
        }

        await RemoveAsync(exercise);
    }

    public async Task UpdateExerciseById(int exerciseId, ExerciseRequest exerciseRequest)
    {
        var exercise = await GetQueryable().SingleOrDefaultAsync(e => e.ExerciseId == exerciseId);

        if (exercise == null)
        {
            throw new Exception("No exercise with such id.");
        }

        Mapper.Map(exerciseRequest, exercise);
        await SaveAsync();
    }
}