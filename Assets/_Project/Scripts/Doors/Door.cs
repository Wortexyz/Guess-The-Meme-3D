using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Meme Display")]
    [SerializeField] private MeshRenderer memeDisplay;

    [Header("Display Material")]
    [SerializeField] private Material memeDisplayMaterial;

    [Header("Door Opening")]
    [SerializeField] private Transform doorHinge;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 180f;

    private MemeData assignedMeme;
    private RoomController room;

    private bool answered;
    private bool opening;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    public MemeData AssignedMeme => assignedMeme;

    private void Awake()
    {
        if (doorHinge != null)
        {
            closedRotation =
                doorHinge.localRotation;

            openRotation =
                closedRotation *
                Quaternion.Euler(
                    0f,
                    openAngle,
                    0f
                );
        }
    }

    private void Update()
    {
        if (!opening || doorHinge == null)
            return;

        doorHinge.localRotation =
            Quaternion.RotateTowards(
                doorHinge.localRotation,
                openRotation,
                openSpeed * Time.deltaTime
            );
    }

    public void Setup(
        MemeData meme,
        RoomController roomController)
    {
        assignedMeme = meme;
        room = roomController;

        answered = false;
        opening = false;

        if (doorHinge != null)
        {
            doorHinge.localRotation =
                closedRotation;
        }

        UpdateMemeDisplay();
    }

    private void UpdateMemeDisplay()
    {
        if (assignedMeme == null)
            return;

        if (assignedMeme.image == null)
            return;

        if (memeDisplay == null)
            return;

        if (memeDisplayMaterial == null)
            return;

        Material materialInstance =
            new Material(
                memeDisplayMaterial
            );

        materialInstance.mainTexture =
            assignedMeme.image.texture;

        memeDisplay.material =
            materialInstance;
    }

    public void PlayerEnteredDoor()
    {
        if (answered)
            return;

        if (assignedMeme == null)
            return;

        if (room == null)
            return;

        answered = true;

        room.DoorSelected(this);
    }

    public void Open()
    {
        if (doorHinge == null)
        {
            Debug.LogWarning(
                "Door Hinge is not assigned on " +
                gameObject.name
            );

            return;
        }

        opening = true;
    }

    public void ResetDoor()
    {
        assignedMeme = null;
        room = null;

        answered = false;
        opening = false;

        if (doorHinge != null)
        {
            doorHinge.localRotation =
                closedRotation;
        }
    }
}