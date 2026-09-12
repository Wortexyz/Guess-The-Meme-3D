using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private MemeDatabase memeDatabase;

    [Header("Category")]
    [SerializeField] private MemeCategory category =
        MemeCategory.All;

    [Header("Audio")]
    [SerializeField] private AudioSource memeAudioSource;

    private QuestionGenerator questionGenerator;

    private int score;

    private void Awake()
    {
        questionGenerator = new QuestionGenerator();
    }

    // Creates a new question for a room.
    public Question CreateQuestion()
    {
        if (memeDatabase == null)
        {
            Debug.LogError(
                "QuestionManager: MemeDatabase is not assigned."
            );

            return null;
        }

        List<MemeData> availableMemes =
            memeDatabase.GetEnabledMemes(category);

        if (availableMemes == null ||
            availableMemes.Count < 3)
        {
            Debug.LogError(
                "QuestionManager: " +
                "At least 3 unique enabled memes are required."
            );

            return null;
        }

        Question question =
            questionGenerator.GenerateQuestion(
                availableMemes
            );

        if (question == null)
        {
            Debug.LogError(
                "QuestionManager: Failed to create question."
            );

            return null;
        }

        Debug.Log(
            "QUESTION CREATED: " +
            question.CorrectAnswer.displayName
        );

        return question;
    }

    // Called by the Room's QuestionTrigger.
    public void StartQuestionAudio(Question question)
    {
        if (question == null)
            return;

        if (memeAudioSource == null)
        {
            Debug.LogWarning(
                "QuestionManager: No AudioSource assigned."
            );

            return;
        }

        AudioClip clip =
            question.CorrectAnswer.audio;

        if (clip == null)
        {
            Debug.LogWarning(
                "QuestionManager: Question has no audio clip."
            );

            return;
        }

        // Prevent overlap.
        memeAudioSource.Stop();

        // Always start from the beginning.
        memeAudioSource.clip = clip;
        memeAudioSource.time = 0f;

        memeAudioSource.Play();
    }

    public void StopQuestionAudio()
    {
        if (memeAudioSource == null)
            return;

        memeAudioSource.Stop();
    }

    // ---------------------------------------------------------
    // SCORE
    // ---------------------------------------------------------

    public void CorrectAnswer()
    {
        score++;

        Debug.Log(
            "CORRECT! Score = " +
            score
        );
    }

    public void WrongAnswer()
    {
        Debug.Log("WRONG!");
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }

    // ---------------------------------------------------------
    // CATEGORY
    // ---------------------------------------------------------

    public void SetCategory(MemeCategory newCategory)
    {
        category = newCategory;
    }

    public MemeCategory GetCategory()
    {
        return category;
    }
}