using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Meme Display")]
    [SerializeField] private MeshRenderer memeDisplay;

    [Header("Display Material")]
    [SerializeField] private Material memeDisplayMaterial;

    private MemeData assignedMeme;
    private QuestionManager questionManager;

    private bool answered;

    public MemeData AssignedMeme => assignedMeme;

    public void Setup(
        MemeData meme,
        QuestionManager manager)
    {
        assignedMeme = meme;
        questionManager = manager;
        answered = false;

        UpdateMemeDisplay();
    }

    private void UpdateMemeDisplay()
    {
        if (assignedMeme == null)
            return;

        if (assignedMeme.image == null)
        {
            Debug.LogWarning(
                "Meme has no image: " +
                assignedMeme.displayName
            );

            return;
        }

        if (memeDisplayMaterial == null)
        {
            Debug.LogError(
                "Meme display material is not assigned."
            );

            return;
        }

        // Create a unique material instance for this door.
        Material materialInstance =
            new Material(memeDisplayMaterial);

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

        if (questionManager == null)
            return;

        answered = true;

        questionManager.DoorSelected(this);
    }

    public void ResetDoor()
    {
        assignedMeme = null;
        questionManager = null;
        answered = false;
    }
}