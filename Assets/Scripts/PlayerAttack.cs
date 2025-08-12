using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Переменные окружения.
    [SerializeField] private Animator anim;
    public bool IsAttacking = false;
    private PlayerMovement movement;
    private float attackX;
    private float attackY;

    // Метод, который вызывается при запуске сцены. Передаёт в переменную movement скрипт, отвечающий за движение игрока.
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }


    // Метод, который вызывается каждый кадр
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsAttacking)
        {
            TriggerAttack();
        }
    }

    // Метод атаки игрока. При нажатии левой кнопки мыши игрок атакует в зависимости от направления своего взгляда.
    public void TriggerAttack()
    {
        Vector2 lookDir = movement.LastLookDir;

        anim.SetBool("IsAttacking", true);
        anim.SetTrigger("TriggerAttack");
        anim.SetFloat("X", lookDir.x);
        anim.SetFloat("Y", lookDir.y);
    }

    // Вызывается ивентом в анимации, чтобы остановить атаку игрока
    public void EndAttack()
    {
        anim.SetBool("IsAttacking", false);
    }

}
