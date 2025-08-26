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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical   = Input.GetAxisRaw("Vertical");
        InputDirection   = new Vector2(horizontal, vertical).normalized;

        if (InputDirection != Vector2.zero && !attack.IsAttacking)
        {
            // если обе оси не равны нулю → приоритет вправо/влево
            if (InputDirection.x != 0 && InputDirection.y != 0)
            {
                LastLookDir = new Vector2(Mathf.Sign(InputDirection.x), 0);
            }
            else if (InputDirection.x != 0)
            {
                LastLookDir = new Vector2(Mathf.Sign(InputDirection.x), 0);
            }
            else if (InputDirection.y != 0)
            {
                LastLookDir = new Vector2(0, Mathf.Sign(InputDirection.y));
            }
        }

        rb.linearVelocity = InputDirection * movementSpeed;
    }

    // Метод, который анимирует игрока в зависимости от его состояния        
    private void Animate()
    {
        if (!attack.IsAttacking)
        {
            if (IsMoving)
            {
                anim.SetFloat("X", LastLookDir.x);
                anim.SetFloat("Y", LastLookDir.y);
            }
        }

        anim.SetBool("Moving", IsMoving);
    }
}