using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Переменные окружения.
    [SerializeField] private Animator anim;
    public bool IsAttacking = false;
    private PlayerMovement movement;

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
        anim.SetBool("IsAttacking", true);
        anim.SetFloat("X", movement.LastX);
        anim.SetFloat("Y", movement.LastY);

        // anim.SetTrigger("isAttacking");
    }

    // Вызывается ивентом в анимации, чтобы остановить атаку игрока
    public void EndAttack()
    {
        anim.SetBool("IsAttacking", false);
    }

}
