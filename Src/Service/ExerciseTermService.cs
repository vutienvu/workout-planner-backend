using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class ExerciseTermService(DatabaseContext databaseContext, IMapper mapper) : IExerciseTermService
{
    public async Task<List<ExerciseTermResponse>> GetAllExerciseTerms()
    {
        var exerciseTerms = await databaseContext.ExerciseTerms.ToListAsync();

        List<ExerciseTermResponse> exerciseTermsResponse = mapper.Map<List<ExerciseTerm>, List<ExerciseTermResponse>>(exerciseTerms);

        return exerciseTermsResponse;
    }

    public async Task<ExerciseTermResponse> CreateExerciseTerm(ExerciseTermRequest exerciseTermRequest)
    {
        var exercise = await databaseContext.Exercises.SingleOrDefaultAsync(e => e.ExerciseId == exerciseTermRequest.ExerciseId);

        if (exercise == null)
        {
            throw new Exception("No exercise with such id.");
        }

        ExerciseTerm newExerciseTerm = mapper.Map<ExerciseTermRequest, ExerciseTerm>(exerciseTermRequest);

        var exerciseTermEntity = await databaseContext.ExerciseTerms.AddAsync(newExerciseTerm);
        await databaseContext.SaveChangesAsync();

        var exerciseTerm = exerciseTermEntity.Entity;

        ExerciseTermResponse exerciseTermResponse = mapper.Map<ExerciseTerm, ExerciseTermResponse>(exerciseTerm);

        return exerciseTermResponse;
    }

    public async Task DeleteExerciseTermById(int exerciseTermId)
    {
        var exerciseTerm = await databaseContext.ExerciseTerms.SingleOrDefaultAsync(et => et.ExerciseTermId == exerciseTermId);

        if (exerciseTerm == null)
        {
            throw new Exception("No exercise term with such id.");
        }

        databaseContext.ExerciseTerms.Remove(exerciseTerm);
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateExerciseTermById(int exerciseTermId, ExerciseTermRequest exerciseTermRequest)
    {
        var exerciseTerm = await databaseContext.ExerciseTerms.SingleOrDefaultAsync(et => et.ExerciseTermId == exerciseTermId);
        var exercise = await databaseContext.Exercises.SingleOrDefaultAsync(e => e.ExerciseId == exerciseTermRequest.ExerciseId);
        
        if (exerciseTerm == null)
        {
            throw new Exception("No exercise term with such id.");
        }

        if (exercise == null)
        {
            throw new Exception("No exercise with such id.");
        }

        mapper.Map(exerciseTermRequest, exerciseTerm);
        await databaseContext.SaveChangesAsync();
    }
}