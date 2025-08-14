
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerAttack : MonoBehaviour
{
    // Переменные окружения.
    [SerializeField] private Animator anim;
    public bool IsAttacking = false;
    private PlayerMovement movement;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    private GameObject attackArea = default;

    // Метод, который вызывается при запуске сцены. Передаёт в переменную movement скрипт, отвечающий за движение игрока.
    private void Start()
    {
        Transform root = transform.root;
        Transform found = root.Find("AttackArea");
        if (found != null)
        {
            attackArea = found.gameObject;
        }
        movement = GetComponent<PlayerMovement>();
    }


    // Метод, который вызывается каждый кадр
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsAttacking)
        {
            TriggerAttack();
        }

        if (IsAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                IsAttacking = false;
                attackArea.SetActive(IsAttacking);
            }
        }
    }

    // Метод атаки игрока. При нажатии левой кнопки мыши игрок атакует в зависимости от направления своего взгляда.
    public void TriggerAttack()
    {
        Vector2 lookDir = movement.LastLookDir;

        IsAttacking = true;
        attackArea.SetActive(IsAttacking);
        anim.SetBool("IsAttacking", true);
        anim.SetTrigger("TriggerAttack");
        anim.SetFloat("X", lookDir.x);
        anim.SetFloat("Y", lookDir.y);
    }

    // Вызывается ивентом в анимации, чтобы остановить атаку игрока
    public void EndAttack()
    {
        IsAttacking = false;
        anim.SetBool("IsAttacking", false);

    }

}
