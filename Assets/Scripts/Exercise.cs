using UnityEngine;

[CreateAssetMenu(fileName = "New Exercise", menuName = "WorkoutManagement/Exercise")]
public class Exercise : ScriptableObject {
    public string exerciseName;
    public float duration; // Duration in seconds

    public void PerformExercise() {
        Debug.Log($"Performing exercise: {exerciseName} for {duration} seconds.");
    }
}