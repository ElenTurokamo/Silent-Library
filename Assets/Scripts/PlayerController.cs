using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Animator anim;
    public Vector2 InputDirection { get; private set; }
    public float X => InputDirection.x;
    public float Y => InputDirection.y;
    public bool IsMoving => InputDirection.magnitude > 0.01f;

    public float LastX { get; private set; }
    public float LastY { get; private set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        Animate();
    }

    private void HandleInput()
    {
        rb.linearVelocity = InputDirection * movementSpeed;
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

        InputDirection = new Vector2(horizontal, vertical).normalized;
    }

    private void Animate()
    {
        if (IsMoving)
        {
            anim.SetFloat("X", X);
            anim.SetFloat("Y", Y);
        }

        anim.SetBool("Moving", IsMoving);
    }
}