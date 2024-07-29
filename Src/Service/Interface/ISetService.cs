using WorkoutPlanner.Request;
using WorkoutPlanner.Response;

namespace WorkoutPlanner.Service.Interface;

public interface ISetService
{
    public Task<List<SetResponse>> GetAllSets();
    public Task<SetResponse> CreateSet(SetRequest setRequest);
    public Task DeleteSetById(int setId);
    public Task UpdateSetById(int setId, SetRequest setRequest);
}