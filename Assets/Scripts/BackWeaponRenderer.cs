using NUnit.Framework;
using UnityEngine;

// Класс, отвечающий за появление и исчезновение оружия за спиной игрока во время атаки.
public class BackWeaponRenderer : MonoBehaviour
{
    // Объект игрока
    public GameObject Character;
    // Слот "оружия"
    public GameObject weaponSprite;

    [Header("Positions")]
    public Vector3 positionUp;
    public Vector3 positionDown;
    public Vector3 positionLeft;
    public Vector3 positionRight;

    [Header("Angles")]
    public Quaternion angleUp;
    public Quaternion angleDown;
    public Quaternion angleLeft;
    public Quaternion angleRight;

    private Animator animator;

    // Метод Start, вызывается один раз при появлении объекта в иерархии
    // Если есть персонаж игрока, то получает его аниматор
    void Start()
    {
        if (Character != null)
        {
            animator = Character.GetComponent<Animator>();
        }
    }

    // Метод Update, вызывается каждый кадр
    // 
    void Update()
    {
        if (animator == null || weaponSprite == null) return;
        
        if (animator.GetBool("IsAttacking"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
