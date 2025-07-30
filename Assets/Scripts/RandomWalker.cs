using UnityEngine;

public class RandomWalker
{
    public Vector2 Position;
    public Vector2 Direction;
    public float ChanceToChange;


    public RandomWalker(Vector2 pos, Vector2 dir, float chanceToChange)
    {
        Position = pos;
        Direction = dir;
        ChanceToChange = chanceToChange;
    }
}