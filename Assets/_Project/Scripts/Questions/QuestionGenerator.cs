using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator
{
    public Question GenerateQuestion(
        List<MemeData> availableMemes)
    {
        if (availableMemes == null)
        {
            Debug.LogError("Meme list is null.");
            return null;
        }

        // Remove duplicate MemeData references / IDs.
        List<MemeData> uniqueMemes =
            GetUniqueMemes(availableMemes);

        if (uniqueMemes.Count < 3)
        {
            Debug.LogError(
                "Not enough UNIQUE memes to create a question. " +
                "Need at least 3."
            );

            return null;
        }

        // Shuffle the available memes.
        Shuffle(uniqueMemes);

        // First meme becomes the correct answer.
        MemeData correctAnswer = uniqueMemes[0];

        // Exactly 3 unique choices.
        List<MemeData> choices = new List<MemeData>
        {
            uniqueMemes[0],
            uniqueMemes[1],
            uniqueMemes[2]
        };

        // Shuffle the door positions.
        Shuffle(choices);

        return new Question(
            correctAnswer,
            choices
        );
    }

    private List<MemeData> GetUniqueMemes(
        List<MemeData> source)
    {
        List<MemeData> result =
            new List<MemeData>();

        HashSet<string> usedIds =
            new HashSet<string>();

        foreach (MemeData meme in source)
        {
            if (meme == null)
                continue;

            // Prefer the ID as the unique identity.
            string id = meme.id;

            // If ID is empty, use the asset's instance ID.
            if (string.IsNullOrWhiteSpace(id))
            {
                id = meme.GetInstanceID().ToString();
            }

            if (usedIds.Contains(id))
                continue;

            usedIds.Add(id);
            result.Add(meme);
        }

        return result;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex =
                Random.Range(0, i + 1);

            T temp = list[i];

            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}