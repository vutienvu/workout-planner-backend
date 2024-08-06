using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class ExerciseTermService(DatabaseContext databaseContext, IMapper mapper) : BaseService<ExerciseTerm>(databaseContext, mapper), IExerciseTermService
{
    public async Task<List<ExerciseTermResponse>> GetAllExerciseTerms()
    {
        var exerciseTerms = await GetQueryable().Include(et => et.Sets).ToListAsync();

        List<ExerciseTermResponse> exerciseTermsResponse = Mapper.Map<List<ExerciseTerm>, List<ExerciseTermResponse>>(exerciseTerms);

        return exerciseTermsResponse;
    }

    public async Task<ExerciseTermResponse> CreateExerciseTerm(ExerciseTermRequest exerciseTermRequest)
    {
        ExerciseTerm newExerciseTerm = Mapper.Map<ExerciseTermRequest, ExerciseTerm>(exerciseTermRequest);
        
        var exerciseTerm = await CreateAsync(newExerciseTerm);

        ExerciseTermResponse exerciseTermResponse = Mapper.Map<ExerciseTerm, ExerciseTermResponse>(exerciseTerm);

        return exerciseTermResponse;
    }

    public async Task DeleteExerciseTermById(int exerciseTermId)
    {
        var exerciseTerm = await GetQueryable().SingleOrDefaultAsync(et => et.ExerciseTermId == exerciseTermId);

        if (exerciseTerm == null)
        {
            throw new Exception("No exercise term with such id.");
        }

        await RemoveAsync(exerciseTerm);
    }

    public async Task UpdateExerciseTermById(int exerciseTermId, ExerciseTermRequest exerciseTermRequest)
    {
        var exerciseTerm = await GetQueryable().SingleOrDefaultAsync(et => et.ExerciseTermId == exerciseTermId);
        
        if (exerciseTerm == null)
        {
            throw new Exception("No exercise term with such id.");
        }

        Mapper.Map(exerciseTermRequest, exerciseTerm);
        await SaveAsync();
    }
}