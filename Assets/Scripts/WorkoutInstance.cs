using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Workout", menuName = "WorkoutManagement/Workout")]
public class WorkoutInstance : ScriptableObject {
    public string workoutName;
    public Exercise[] exercises;
    
}