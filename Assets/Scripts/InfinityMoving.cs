using UnityEngine;

public class InfinityMoving : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 0.4f;
    public Vector2 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }
    void Update()
    {
        float t = Time.time * speed;

        float denominator = 1f + Mathf.Pow(Mathf.Sin(t), 2f);
        float x = (Mathf.Cos(t) / denominator) * amplitude;
        float y = (Mathf.Cos(t) * Mathf.Sin(t) / denominator) * amplitude;

        transform.position = new Vector2(startingPosition.x + x, startingPosition.y + y);
    }
}
