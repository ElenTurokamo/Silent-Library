using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Animator anim;
    public Vector2 InputDirection { get; private set; }
    public float X => InputDirection.x;
    public float Y => InputDirection.y;
    public bool IsMoving => InputDirection.magnitude > 0.01f;
    public bool isAttacking = false;

    public float LastX { get; private set; }
    public float LastY { get; private set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetMovingDirection();
        Attack();
        Animate();
    }

    private void GetMovingDirection()
    {
        rb.linearVelocity = InputDirection * movementSpeed;
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKeyDown(KeyCode.A))
            LastX = -1;
        if (Input.GetKeyDown(KeyCode.D))
            LastX = 1;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            horizontal = -1;
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            horizontal = 1;
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            horizontal = LastX;

        if (Input.GetKeyDown(KeyCode.W))
            LastY = 1;
        if (Input.GetKeyDown(KeyCode.S))
            LastY = -1;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            vertical = 1;
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            vertical = -1;
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            vertical = LastY;

        // if (Input.GetKey(KeyCode.Mouse0) && !isAttacking)
        // {
        //     isAttacking = true;
        //     anim.ResetTrigger("IsAttacking");
        //     anim.SetTrigger("IsAttacking");
        // }

        InputDirection = new Vector2(horizontal, vertical).normalized;
    }

    public void Attack()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isAttacking)
        {
            LastY = 1;
            isAttacking = true;
            // IsMoving = false;
            anim.ResetTrigger("IsAttacking");
            anim.SetTrigger("IsAttacking");
        }
        // if (Input.GetKey(KeyCode.Mouse0) && !isAttacking && Input.GetKey(KeyCode.A))
        // {
        //     LastX = -1;
        //     isAttacking = true;
        //     anim.ResetTrigger("IsAttacking");
        //     anim.SetTrigger("IsAttacking");
        // }
        // if (Input.GetKey(KeyCode.Mouse0) && !isAttacking && Input.GetKey(KeyCode.S))
        // {
        //     LastY = -1;
        //     isAttacking = true;
        //     anim.ResetTrigger("IsAttacking");
        //     anim.SetTrigger("IsAttacking");
        // }
        // if (Input.GetKey(KeyCode.Mouse0) && !isAttacking && Input.GetKey(KeyCode.D))
        // {
        //     LastX = 1;
        //     isAttacking = true;
        //     anim.ResetTrigger("IsAttacking");
        //     anim.SetTrigger("IsAttacking");
        // }
    }

    //Вызывается ивентом в анимации, чтобы остановить атаку игрока.
    public void EndAttack()
    {
        isAttacking = false;
        // anim.SetFloat("X", X);
        // anim.SetFloat("Y", Y);
    }

    private void Animate()
    {
        if (IsMoving)
        {
            anim.SetFloat("X", X);
            anim.SetFloat("Y", Y);
        }
        anim.SetBool("Moving", IsMoving);

        if (isAttacking)
        {
            anim.SetFloat("X", X);
            anim.SetFloat("Y", Y);
        }
        anim.SetBool("IsAttacking", isAttacking);
    }
}