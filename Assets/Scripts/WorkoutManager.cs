using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class WorkoutManager : MonoBehaviour
{
    public WorkoutInstance defaultWorkout;

    [Header("Workout Related")]
    public bool playing;
    public float globalTimer = 0f;
    public float exerciseTimer = 0f;
    public WorkoutInstance currentWorkout;
    public Exercise currentExercise;
    private int exercisesCount;
    private int nextExerciseIndex;

    [Header("Sounds")]
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip resetClip;
    public AudioClip nextExerciseClip;
    public AudioClip startNewWorkoutClip;
    public AudioClip skipExerciseClip;

    [Header("UI")]
    public TextMeshProUGUI globalTimerText;
    public TextMeshProUGUI exerciseTimerText;
    public TextMeshProUGUI exerciseNameText;
    public TextMeshProUGUI workoutNameText;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartWorkout(WorkoutInstance workout)
    {
        currentWorkout = workout;
        globalTimer = 0;
        exercisesCount = workout.exercises.Length;
        nextExerciseIndex = 1;

        if (workout != null && exercisesCount > 0)
        {
            currentExercise = workout.exercises[0];
            PlaySound(startNewWorkoutClip);
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
                    PlaySound(nextExerciseClip);
                }
            }
        }

        if (currentWorkout != null)
        {
            globalTimerText.text = string.Format("{0:00}:{1:00}", (int)globalTimer / 60, (int)globalTimer % 60);
            
            if (currentExercise != null)
            {
                float remainingTime = Mathf.Max(0, currentExercise.duration - exerciseTimer);
                exerciseTimerText.text = string.Format("{0:00}:{1:00}", (int)remainingTime / 60, (int)remainingTime % 60);
                exerciseNameText.text = currentExercise.exerciseName;
            }
            workoutNameText.text = $"Workout : {currentWorkout.workoutName}";
        }
        else
        {
            globalTimerText.text = " --- ";
            exerciseTimerText.text = " --- ";
            exerciseNameText.text = " --- ";
            workoutNameText.text = " --- ";
        }
    }

    public void Pause()
    {
        if (playing)
        {
            playing = false;
            PlaySound(pauseClip);
        }
    }

    public void Play()
    {
        if (!playing)
        {
            if (currentWorkout == null) 
            {
                StartWorkout(defaultWorkout);
            }
            else
            {
                PlaySound(resumeClip);
            }
            playing = true;
        }
    }

    public void SkipBack()
    {
        if (currentWorkout == null) return;
        
        PlaySound(skipExerciseClip);

        if (exerciseTimer >= 1 || nextExerciseIndex <= 1) {
            exerciseTimer = 0;
            return;
        }

        exerciseTimer = 0;
        nextExerciseIndex--;
        currentExercise = currentWorkout.exercises[nextExerciseIndex - 1];
    }

    public void SkipForth()
    {
        if (currentWorkout == null) return;
        
        if (nextExerciseIndex >= exercisesCount)
        {
            Reset();
            return;
        }

        PlaySound(skipExerciseClip);

        exerciseTimer = 0;
        currentExercise = currentWorkout.exercises[nextExerciseIndex++];
    }

    public void Reset()
    {
        PlaySound(resetClip);

        currentWorkout = null;
        currentExercise = null;
        globalTimer = 0;
        exerciseTimer = 0;
        nextExerciseIndex = 0;
        playing = false;
        exercisesCount = 0;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}