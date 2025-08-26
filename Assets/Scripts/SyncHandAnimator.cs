using UnityEngine;

// Класс, отвечающий за синхронизацию аниматора рук на основе тела игрока.
public class SyncHandAnimator : MonoBehaviour
{
    public Animator sourceAnimator;     // Аниматор тела
    private Animator thisAnimator;    // Аниматор рук
    private PlayerMovement playerMovement; // ссылка на скрипт движения игрока

    // Метод Start, вызывается при появлении объекта в иерархии
    // Получает аниматор прикреплённой руки.
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Метод Update, вызывается каждый кадр
    // Получает состояние аниматора тела и переводит аниматоры рук в то же состояние
    void Update()
    {
        var state = sourceAnimator.GetCurrentAnimatorStateInfo(0);
        thisAnimator.Play(state.fullPathHash, 0, state.normalizedTime);
    
        if (playerMovement != null)
        {
            Vector2 dir = playerMovement.LastLookDir;
            thisAnimator.SetFloat("X", dir.x);
            thisAnimator.SetFloat("Y", dir.y);
        }
    }
}