using System.Collections.Generic;

public class Question
{
    public MemeData CorrectAnswer { get; }

    public List<MemeData> Choices { get; }

    public Question(
        MemeData correctAnswer,
        List<MemeData> choices)
    {
        CorrectAnswer = correctAnswer;
        Choices = choices;
    }
}