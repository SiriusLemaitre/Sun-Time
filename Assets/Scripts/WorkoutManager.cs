using UnityEngine;
using System.Collections;
using TMPro;

public class WorkoutManager : MonoBehaviour
{
    [Header ("Workout Related")]
    public bool playing;
    public float globalTimer = 0f;
    public float exerciceTimer = 0f;
    public WorkoutInstance currentWorkout;
    public Exercise currentExercise;
    private int exercicesCount;
    private int nextExerciseIndex;

    [Header ("Sounds")]
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip resetClip;
    public AudioClip nextExerciseClip;
    public AudioClip startNewWorkoutClip;
    public AudioClip skipExerciceClip;

    [Header ("UI")]
    public TextMeshProUGUI globalTimerText;
    public TextMeshProUGUI exerciseTimerText;
    public TextMeshProUGUI exerciseNameText;
    public TextMeshProUGUI workoutNameText;

    public void StartWorkout(WorkoutInstance workout)
    {
        currentWorkout = workout;
        globalTimer = 0;
        exercicesCount = workout.exercises.Length;
        nextExerciseIndex = 1;

        if (workout != null && exercicesCount > 0)
        {
            currentExercise = workout.exercises[0];
        }
    }

    void Update()
    {
        if (playing == true && currentWorkout != null)
        {
            globalTimer += Time.deltaTime;
            exerciceTimer += Time.deltaTime;

            if (exerciceTimer >= currentExercise.duration)
            {
                exerciceTimer = 0;

                if (nextExerciseIndex >= exercicesCount)
                {
                    Reset();
                }
                else
                {
                    currentExercise = currentWorkout.exercises[nextExerciseIndex++];
                }
            }
        }

        globalTimerText.text = $"Workout : {Mathf.RoundToInt(globalTimer)}";
        exerciseTimerText.text = $"{Mathf.RoundToInt(globalTimer)}";
        exerciseNameText.text = currentExercise != null ? currentExercise.exerciseName : " --- ";
        workoutNameText.text = "Workout : " + currentWorkout != null ? currentWorkout.workoutName : " --- ";


    }

    public void Pause()
    {
        playing = false;
    }

    public void Play()
    {
        playing = true;
    }

    public void Reset()
    {
        currentWorkout = null;
        currentExercise = null;
        globalTimer = 0;
        playing = false;
        exercicesCount = 0;
    }


}