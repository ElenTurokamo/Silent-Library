using UnityEngine;

public class PlayerArmsController : MonoBehaviour
{

    [SerializeField] public Animator anim;

    private Vector2 input;
    private bool moving;
    private float x;
    private float y;
    private float LastX;
    private float LastY;

    void Update()
    {
        GetInput();
        Animate();
    }

    private void GetInput()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            LastX = -1;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            LastX = 1;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            horizontal = -1;
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            horizontal = 1;
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            horizontal = LastX;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            LastY = 1;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            LastY = -1;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            vertical = 1;
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            vertical = -1;
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            vertical = LastY;

        x = horizontal;
        y = vertical;

        input = new Vector2(x, y).normalized;
    }
    private void Animate()
    {
        if (input.magnitude > 0.1f || input.magnitude < -0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }

        anim.SetBool("Moving", moving);
    }
}
