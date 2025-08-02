using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Animator anim;
    public GetPlayerInputState inputState;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Animate();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = inputState.InputDirection * movementSpeed;
    }

    private void Animate()
    {
        if (inputState.IsMoving)
        {
            anim.SetFloat("X", inputState.X);
            anim.SetFloat("Y", inputState.Y);
        }

        anim.SetBool("Moving", inputState.IsMoving);
    }
}