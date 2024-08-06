using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
using WorkoutPlanner.Request;
using WorkoutPlanner.Response;
using WorkoutPlanner.Service.Interface;

namespace WorkoutPlanner.Service;

public class SetService(DatabaseContext databaseContext, IMapper mapper) : BaseService<Set>(databaseContext, mapper), ISetService
{
    public async Task<List<SetResponse>> GetAllSets()
    {
        var sets = await GetQueryable().ToListAsync();

        var setsResponse = Mapper.Map<List<Set>, List<SetResponse>>(sets);
        
        return setsResponse;
    }

    public async Task<SetResponse> CreateSet(SetRequest setRequest)
    {
        Set newSet = Mapper.Map<SetRequest, Set>(setRequest);

        var set = await CreateAsync(newSet);

        SetResponse setResponse = Mapper.Map<Set, SetResponse>(set);

        return setResponse;
    }

    public async Task DeleteSetById(int setId)
    {
        var set = await GetQueryable().SingleOrDefaultAsync(s => s.SetId == setId);

        if (set == null)
        {
            throw new Exception("No set with such id.");
        }

        await RemoveAsync(set);
    }

    public async Task UpdateSetById(int setId, SetRequest setRequest)
    {
        var set = await GetQueryable().SingleOrDefaultAsync(s => s.SetId == setId);

        if (set == null)
        {
            throw new Exception("No set with such id.");
        }

        Mapper.Map(setRequest, set);
        await SaveAsync();
    }
}