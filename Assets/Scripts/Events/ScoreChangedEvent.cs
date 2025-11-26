using UnityEngine;

public class ScoreChanged : IEvent
{
    private string _description;

    public ScoreChanged(int score, string description)
    {
        Score = score;
        _description = description;
    }

    public int Score {  get; private set; }

    public string GetDescription()
    {
        return _description;
    }
}
