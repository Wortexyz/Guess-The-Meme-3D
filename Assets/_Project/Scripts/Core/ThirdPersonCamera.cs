using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera Position")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 3.5f, -6f);

    [Header("Look At")]
    [SerializeField] private float lookHeight = 1.5f;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Follow the player directly.
        transform.position = target.position + offset;

        // Look at the player.
        Vector3 lookPosition =
            target.position + Vector3.up * lookHeight;

        transform.LookAt(lookPosition);
    }
}