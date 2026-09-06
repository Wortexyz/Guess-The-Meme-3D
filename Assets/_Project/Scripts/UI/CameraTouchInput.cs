using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Cinemachine;

public class CameraTouchInput : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    [Header("Cinemachine")]
    [SerializeField] private CinemachinePanTilt panTilt;

    [Header("Sensitivity")]
    [SerializeField] private float horizontalSensitivity = 0.3f;
    [SerializeField] private float verticalSensitivity = 0.2f;

    [Header("Return")]
    [SerializeField] private float returnSpeed = 8f;

    private Vector2 lastPosition;
    private bool touching;

    private void Update()
    {
        // When the player is not touching the camera area,
        // return camera toward the target's forward direction.
        if (!touching && panTilt != null)
        {
            panTilt.PanAxis.Value = Mathf.LerpAngle(
                panTilt.PanAxis.Value,
                0f,
                returnSpeed * Time.deltaTime
            );

            panTilt.TiltAxis.Value = Mathf.Lerp(
                panTilt.TiltAxis.Value,
                0f,
                returnSpeed * Time.deltaTime
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touching = true;
        lastPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!touching || panTilt == null)
            return;

        Vector2 currentPosition = eventData.position;
        Vector2 delta = currentPosition - lastPosition;

        lastPosition = currentPosition;

        // Horizontal
        panTilt.PanAxis.Value +=
            delta.x * horizontalSensitivity;

        // Vertical
        // Swipe UP = look UP.
        panTilt.TiltAxis.Value -=
            delta.y * verticalSensitivity;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touching = false;
    }
}