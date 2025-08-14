
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
    private SyncHandAnimator syncHandAnimator;

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
        Vector2 direction;
        Vector2 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cursorDir = (cursorWorldPos - (Vector2)transform.position).normalized;

        if (cursorDir.magnitude > 0.01f)
        {
            direction = cursorDir;
            syncHandAnimator.DetachAndAim(cursorWorldPos);

            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
        }
        else
        {
            direction = movement.LastLookDir;

            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle > -45 && angle <= 45) anim.SetTrigger("AttackRight");
        else if (angle > 45 && angle <= 135) anim.SetTrigger("AttackUp");
        else if (angle < -45 && angle >= -135) anim.SetTrigger("AttackDown");
        else anim.SetTrigger("AttackLeft");

        IsAttacking = true;
        attackArea.SetActive(true);
        anim.SetBool("IsAttacking", true);
    }

    // Вызывается ивентом в анимации, чтобы остановить атаку игрока
    public void EndAttack()
    {
        anim.SetBool("IsAttacking", false);
    }

}
