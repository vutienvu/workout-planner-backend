using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class ExerciseService(DatabaseContext databaseContext, IMapper mapper) : IExerciseService
{
    public async Task<List<ExerciseResponse>> GetAllExercises()
    {
        var exercises = await databaseContext.Exercises.ToListAsync();

        List<ExerciseResponse> exercisesResponse = mapper.Map<List<Exercise>, List<ExerciseResponse>>(exercises);

        return exercisesResponse;
    }

    public async Task<ExerciseResponse> CreateExercise(ExerciseRequest exerciseRequest)
    {
        var workout = await databaseContext.Workouts.SingleOrDefaultAsync(w => w.WorkoutId == exerciseRequest.WorkoutId);

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        Exercise newExercise = mapper.Map<ExerciseRequest, Exercise>(exerciseRequest);

        var exerciseEntity = await databaseContext.Exercises.AddAsync(newExercise);
        await databaseContext.SaveChangesAsync();
        
        var exercise = exerciseEntity.Entity;

        ExerciseResponse exerciseResponse = mapper.Map<Exercise, ExerciseResponse>(exercise);

        return exerciseResponse;
    }

    public async Task DeleteExerciseById(int exerciseId)
    {
        var exercise = await databaseContext.Exercises.SingleOrDefaultAsync(e => e.ExerciseId == exerciseId);

        if (exercise == null)
        {
            throw new Exception("No exercise with such id.");
        }

        databaseContext.Exercises.Remove(exercise);
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateExerciseById(int exerciseId, ExerciseRequest exerciseRequest)
    {
        var exercise = await databaseContext.Exercises.SingleOrDefaultAsync(e => e.ExerciseId == exerciseId);
        var workout = await databaseContext.Workouts.SingleOrDefaultAsync(w => w.WorkoutId == exerciseRequest.WorkoutId);

        if (exercise == null)
        {
            throw new Exception("No exercise with such id.");
        }

        if (workout == null)
        {
            throw new Exception("No workout with such id.");
        }

        mapper.Map(exerciseRequest, exercise);
        await databaseContext.SaveChangesAsync();
    }
}