using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "MemeDatabase",
    menuName = "Meme Game/Meme Database"
)]
public class MemeDatabase : ScriptableObject
{
    public List<MemeData> memes =
        new List<MemeData>();

    public List<MemeData> GetEnabledMemes(
        MemeCategory selectedCategory)
    {
        List<MemeData> result =
            new List<MemeData>();

        foreach (MemeData meme in memes)
        {
            if (meme == null)
                continue;

            if (!meme.enabled)
                continue;

            // ALL = accept every category.
            if (selectedCategory != MemeCategory.All)
            {
                if (meme.category != selectedCategory)
                    continue;
            }

            result.Add(meme);
        }

        return result;
    }
}