using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
// Переменные окружения
    public float movementSpeed;
    public Animator anim;
    public Vector2 InputDirection { get; private set; }
    public float X => InputDirection.x;
    public float Y => InputDirection.y;
    public bool IsMoving => InputDirection.magnitude > 0.01f;
    private PlayerAttack attack;
    public float LastX { get; private set; }
    public float LastY { get; private set; }
    public Vector2 LastLookDir { get; private set; } = Vector2.down;

    private Rigidbody2D rb;

    // Метод Start, вызывается при создании игрока
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attack = GetComponent<PlayerAttack>();
    }

    // Метод Update, вызывается каждый кадр.
    // Отвечает за получение направления движения и анимацию игрока.
    private void Update()
    {
        GetMovingDirection();
        Animate();
    }

    // Метод, который получает направление движения игрока в зависимости от нажатой клавиши.
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

        InputDirection = new Vector2(horizontal, vertical).normalized;

        if (InputDirection != Vector2.zero && !attack.IsAttacking)
        {
            LastLookDir = InputDirection;
        }
    }


    // Метод, который анимирует игрока в зависимости от его состояния        
    private void Animate()
    {
        if (!attack.IsAttacking)
        {
            if (IsMoving)
            {
                anim.SetFloat("X", X);
                anim.SetFloat("Y", Y);
            }
        }

        anim.SetBool("Moving", IsMoving);
    }
}