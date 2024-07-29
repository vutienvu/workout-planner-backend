using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class SetService(DatabaseContext databaseContext, IMapper mapper) : ISetService
{
    public async Task<List<SetResponse>> GetAllSets()
    {
        var sets = await databaseContext.Sets.ToListAsync();

        var setsResponse = mapper.Map<List<Set>, List<SetResponse>>(sets);
        
        return setsResponse;
    }

    public async Task<SetResponse> CreateSet(SetRequest setRequest)
    {
        var exerciseTerm = await databaseContext.ExerciseTerms.SingleOrDefaultAsync(et => et.ExerciseTermId == setRequest.ExerciseTermId);

        if (exerciseTerm == null)
        {
            throw new Exception("No exercise term with such id.");
        }

        Set newSet = mapper.Map<SetRequest, Set>(setRequest);

        var setEntity = await databaseContext.Sets.AddAsync(newSet);
        await databaseContext.SaveChangesAsync();

        var set = setEntity.Entity;

        SetResponse setResponse = mapper.Map<Set, SetResponse>(set);

        return setResponse;
    }

    public async Task DeleteSetById(int setId)
    {
        var set = await databaseContext.Sets.SingleOrDefaultAsync(s => s.SetId == setId);

        if (set == null)
        {
            throw new Exception("No set with such id.");
        }

        databaseContext.Sets.Remove(set);
        await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateSetById(int setId, SetRequest setRequest)
    {
        var set = await databaseContext.Sets.SingleOrDefaultAsync(s => s.SetId == setId);
        var exerciseTerm = await databaseContext.ExerciseTerms.SingleOrDefaultAsync(et => et.ExerciseTermId == setRequest.ExerciseTermId);

        if (set == null)
        {
            throw new Exception("No set with such id.");
        }

        if (exerciseTerm == null)
        {
            throw new Exception("No exercise term with such id.");
        }

        mapper.Map(setRequest, set);
        await databaseContext.SaveChangesAsync();
    }
}