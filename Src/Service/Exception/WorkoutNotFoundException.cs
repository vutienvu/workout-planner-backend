namespace WorkoutPlanner.Service.Exception;

public class WorkoutNotFoundException(string message = "Workout not found") : NotFoundException(message);