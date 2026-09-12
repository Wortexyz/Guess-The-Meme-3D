using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    [SerializeField] private RoomController room;

    public void SetRoom(RoomController roomController)
    {
        room = roomController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (room == null)
        {
            Debug.LogError(
                "QuestionTrigger: RoomController is not assigned."
            );

            return;
        }

        room.StartQuestionAudio();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (room == null)
            return;

        room.StopQuestionAudio();
    }
}