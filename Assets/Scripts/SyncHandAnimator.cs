using UnityEngine;
using UnityEngine.InputSystem.Utilities;

// Класс, отвечающий за синхронизацию аниматора рук на основе тела игрока.
public class SyncHandAnimator : MonoBehaviour
{
    public Animator sourceAnimator;     // Аниматор тела
    private Animator myAnimator;    // Аниматор рук
    private bool isDetached = false;    // Изначально аниматор рук всегда привязан к телу
    private Transform handTransform;

    // Метод Start, вызывается при появлении объекта в иерархии
    // Получает аниматор прикреплённой руки.
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        handTransform = transform;
    }

    // Метод Update, вызывается каждый кадр
    // Получает состояние аниматора тела и переводит аниматоры рук в то же состояние
    void Update()
    {
        if (!isDetached)
        {
            var state = sourceAnimator.GetCurrentAnimatorStateInfo(0);
            myAnimator.Play(state.fullPathHash, 0, state.normalizedTime);
        }
    }

    public void DetachAndAim(Vector2 targetPosition)
    {
        isDetached = true;
        Vector2 direction = (targetPosition - (Vector2)handTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        handTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ReAttach()
    {
        isDetached = false;
    }
}