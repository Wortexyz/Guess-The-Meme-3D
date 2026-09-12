using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Room Doors")]
    [SerializeField] private Door[] doors;

    [Header("Question Trigger")]
    [SerializeField] private QuestionTrigger questionTrigger;

    private Question currentQuestion;
    private QuestionManager questionManager;

    public Door[] Doors => doors;

    public Question CurrentQuestion => currentQuestion;

    public void SetupQuestion(QuestionManager manager)
    {
        if (manager == null)
        {
            Debug.LogError(
                "RoomController: QuestionManager is missing."
            );

            return;
        }

        if (doors == null || doors.Length != 3)
        {
            Debug.LogError(
                gameObject.name +
                ": Exactly 3 doors are required."
            );

            return;
        }

        questionManager = manager;

        // Connect this room's QuestionTrigger at runtime.
        if (questionTrigger != null)
        {
            questionTrigger.SetRoom(this);
        }
        else
        {
            Debug.LogWarning(
                gameObject.name +
                ": QuestionTrigger is not assigned."
            );
        }

        currentQuestion =
            questionManager.CreateQuestion();

        if (currentQuestion == null)
        {
            Debug.LogError(
                gameObject.name +
                ": Failed to create question."
            );

            return;
        }

        if (currentQuestion.Choices == null ||
            currentQuestion.Choices.Count != 3)
        {
            Debug.LogError(
                gameObject.name +
                ": Question must contain exactly 3 choices."
            );

            return;
        }

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i] == null)
            {
                Debug.LogError(
                    gameObject.name +
                    ": Door " +
                    (i + 1) +
                    " is not assigned."
                );

                return;
            }

            MemeData meme =
                currentQuestion.Choices[i];

            if (meme == null)
            {
                Debug.LogError(
                    gameObject.name +
                    ": Question choice " +
                    (i + 1) +
                    " is null."
                );

                return;
            }

            doors[i].Setup(
                meme,
                this
            );
        }

        Debug.Log(
            gameObject.name +
            " → QUESTION: " +
            currentQuestion.CorrectAnswer.displayName
        );
    }

    public void StartQuestionAudio()
    {
        if (currentQuestion == null)
            return;

        if (questionManager == null)
            return;

        questionManager.StartQuestionAudio(
            currentQuestion
        );
    }

    public void StopQuestionAudio()
    {
        if (questionManager == null)
            return;

        questionManager.StopQuestionAudio();
    }

    public void DoorSelected(Door selectedDoor)
    {
        if (currentQuestion == null)
            return;

        if (selectedDoor == null)
            return;

        if (questionManager == null)
            return;

        if (selectedDoor.AssignedMeme ==
            currentQuestion.CorrectAnswer)
        {
            questionManager.CorrectAnswer();

            selectedDoor.Open();
        }
        else
        {
            questionManager.WrongAnswer();
        }
    }
}