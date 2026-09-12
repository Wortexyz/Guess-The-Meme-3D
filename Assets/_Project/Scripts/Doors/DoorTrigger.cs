using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (door == null)
        {
            Debug.LogError(
                "DoorTrigger: Door reference is missing on " +
                gameObject.name
            );

            return;
        }

        door.PlayerEnteredDoor();
    }
}