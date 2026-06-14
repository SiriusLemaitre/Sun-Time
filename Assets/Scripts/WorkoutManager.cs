using UnityEngine;
using System.Collections;
using TMPro;

public class WorkoutManager : MonoBehaviour
{
    public WorkoutInstance defaultWorkout;

    [Header ("Workout Related")]
    public bool playing;
    public float globalTimer = 0f;
    public float exerciseTimer = 0f;
    public WorkoutInstance currentWorkout;
    public Exercise currentExercise;
    private int exercisesCount;
    private int nextExerciseIndex;

    [Header ("Sounds")]
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip resetClip;
    public AudioClip nextExerciseClip;
    public AudioClip startNewWorkoutClip;
    public AudioClip skipExerciseClip;

    [Header ("UI")]
    public TextMeshProUGUI globalTimerText;
    public TextMeshProUGUI exerciseTimerText;
    public TextMeshProUGUI exerciseNameText;
    public TextMeshProUGUI workoutNameText;

    public void StartWorkout(WorkoutInstance workout)
    {
        currentWorkout = workout;
        globalTimer = 0;
        exercisesCount = workout.exercises.Length;
        nextExerciseIndex = 1;

        if (workout != null && exercisesCount > 0)
        {
            currentExercise = workout.exercises[0];
        }
    }

    void Update()
    {
        if (playing == true && currentWorkout != null)
        {
            globalTimer += Time.deltaTime;
            exerciseTimer += Time.deltaTime;

            if (exerciseTimer >= currentExercise.duration)
            {
                exerciseTimer = 0;

                if (nextExerciseIndex >= exercisesCount)
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
        if (currentWorkout == null) StartWorkout(defaultWorkout);
        playing = true;
    }

    public void SkipBack()
    {
        if (currentWorkout == null) return;
        if (exerciseTimer >= 1 || nextExerciseIndex <= 1) exerciseTimer = 0;

        exerciseTimer = 0;
        nextExerciseIndex--;
        currentExercise = currentWorkout.exercises[nextExerciseIndex-1];
    }

    public void SkipForth()
    {
        if (currentWorkout == null) return;
        if (nextExerciseIndex >= exercisesCount)
        {
            Reset();
            return;
        }

        exerciseTimer = 0;
        currentExercise = currentWorkout.exercises[nextExerciseIndex++];
    }

    public void Reset()
    {
        currentWorkout = null;
        currentExercise = null;
        globalTimer = 0;
        playing = false;
        exercisesCount = 0;
    }


}