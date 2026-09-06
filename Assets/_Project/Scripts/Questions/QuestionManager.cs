using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private MemeDatabase memeDatabase;

    [Header("Doors")]
    [SerializeField] private Door[] doors;

    [Header("Category")]
    [SerializeField] private MemeCategory category =
        MemeCategory.All;

    [Header("Audio")]
    [SerializeField] private AudioSource memeAudioSource;

    private QuestionGenerator questionGenerator;

    private Question currentQuestion;

    private int score;

    private void Awake()
    {
        questionGenerator = new QuestionGenerator();
    }

    private void Start()
    {
        GenerateNextQuestion();
    }

    private void GenerateNextQuestion()
    {
        List<MemeData> availableMemes =
            memeDatabase.GetEnabledMemes(category);

        if (availableMemes.Count < 3)
        {
            Debug.LogError(
                "QuestionManager: " +
                "At least 3 enabled memes are required."
            );

            return;
        }

        currentQuestion =
            questionGenerator.GenerateQuestion(
                availableMemes
            );

        if (currentQuestion == null)
            return;

        AssignQuestionToDoors();

        PlayQuestionAudio();

        Debug.Log(
            "QUESTION: " +
            currentQuestion.CorrectAnswer.displayName
        );
    }

    private void AssignQuestionToDoors()
    {
        if (doors == null || doors.Length != 3)
        {
            Debug.LogError(
                "QuestionManager requires exactly 3 doors."
            );

            return;
        }

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Setup(
                currentQuestion.Choices[i],
                this
            );

            Debug.Log(
                "Door " +
                (i + 1) +
                ": " +
                currentQuestion.Choices[i].displayName
            );
        }
    }

    private void PlayQuestionAudio()
    {
        if (memeAudioSource == null)
        {
            Debug.LogWarning(
                "No meme AudioSource assigned."
            );

            return;
        }

        AudioClip clip =
            currentQuestion.CorrectAnswer.audio;

        if (clip == null)
        {
            Debug.LogWarning(
                "Correct meme has no audio clip."
            );

            return;
        }

        memeAudioSource.Stop();
        memeAudioSource.clip = clip;
        memeAudioSource.Play();
    }

    public void DoorSelected(Door selectedDoor)
    {
        if (currentQuestion == null)
            return;

        if (selectedDoor.AssignedMeme ==
            currentQuestion.CorrectAnswer)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer(selectedDoor);
        }
    }

   private void CorrectAnswer()
{
    score++;

    Debug.Log(
        "CORRECT! Score = " +
        score
    );

    // Later:
    // Door opens
    // Player enters next room
    // RoomManager generates next question
}

    private void WrongAnswer(Door selectedDoor)
    {
        Debug.Log(
            "WRONG! You selected: " +
            selectedDoor.AssignedMeme.displayName
        );
    }
}