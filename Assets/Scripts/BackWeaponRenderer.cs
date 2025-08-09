using NUnit.Framework;
using UnityEngine;

// Класс, отвечающий за появление и исчезновение оружия за спиной игрока во время атаки.
public class BackWeaponRenderer : MonoBehaviour
{
    // Объект игрока
    public GameObject Character;
    // Слот "оружия"
    public GameObject weaponSprite;

    // Настраиваемые параметры
    [Header("Parameters")]
    [SerializeField] private float fadeSpeed = 9f;

    // Положение меча относительно игрока
    [Header("Positions")]
    public Vector3 positionUp;
    public Vector3 positionDown;
    public Vector3 positionLeft;
    public Vector3 positionRight;

    // Угол меча относительно игрока
    [Header("Angles")]
    public Quaternion angleUp;
    public Quaternion angleDown;
    public Quaternion angleLeft;
    public Quaternion angleRight;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Метод Start, вызывается один раз при появлении объекта в иерархии
    // Если есть персонаж игрока, то получает его аниматор
    void Start()
    {
        if (Character != null)
        {
            animator = Character.GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    // Метод Update, вызывается каждый кадр
    // На основе состояния аниматора игрока убирает меч из-за спины, а также меняет его положение
    // в пространстве в зависимости от положения игрока.
    void Update()
    {
        if (animator == null || weaponSprite == null) return;
        
        if (animator.GetBool("IsAttacking"))
        {
            Color c = spriteRenderer.color;
            c.a = 0f;
            spriteRenderer.color = c;
        }
        else
        {
            Color c = spriteRenderer.color;
            c.a = Mathf.MoveTowards(c.a, 1f, fadeSpeed * Time.deltaTime);
            spriteRenderer.color = c;
        }

        float moveX = animator.GetFloat("X");
        float moveY = animator.GetFloat("Y");

        if (moveY > 0.5f)
        {
            transform.localPosition = positionUp;
            transform.localRotation = angleUp;
        }
        else if (moveY < -0.5f)
        {
            transform.localPosition = positionDown;
            transform.localRotation = angleDown;
        }
        else if (moveX > 0.5f)
        {
            transform.localPosition = positionRight;
            transform.localRotation = angleRight;
        }
        else if (moveX < -0.5f)
        {
            transform.localPosition = positionLeft;
            transform.localRotation = angleLeft;
        }
    }
}
