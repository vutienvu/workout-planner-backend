using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Entity;
using WorkoutPlanner.Helper;
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
}