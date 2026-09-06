using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isCorrect;

    private bool answered;

    private void OnTriggerEnter(Collider other)
    {
        if (answered)
            return;

        if (!other.CompareTag("Player"))
            return;

        answered = true;

        if (isCorrect)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    private void CorrectAnswer()
    {
        Debug.Log("CORRECT DOOR!");
    }

    private void WrongAnswer()
    {
        Debug.Log("WRONG DOOR!");
    }
}
