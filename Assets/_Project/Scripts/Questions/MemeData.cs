using UnityEngine;

[CreateAssetMenu(
    fileName = "Meme_",
    menuName = "Meme Game/Meme Data"
)]
public class MemeData : ScriptableObject
{
    [Header("Identity")]
    public string id;
    public string displayName;

    [Header("Content")]
    public Sprite image;
    public AudioClip audio;

    [Header("Classification")]
    public MemeCategory category;
    public Difficulty difficulty;

    [Header("Availability")]
    public bool enabled = true;
}