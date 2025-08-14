using UnityEngine;
using UnityEngine.InputSystem.Utilities;

// Класс, отвечающий за синхронизацию аниматора рук на основе тела игрока.
public class SyncHandAnimator : MonoBehaviour
{
    public Animator sourceAnimator;     // Аниматор тела
    private Animator myAnimator;    // Аниматор рук

    // Метод Start, вызывается при появлении объекта в иерархии
    // Получает аниматор прикреплённой руки.
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Метод Update, вызывается каждый кадр
    // Получает состояние аниматора тела и переводит аниматоры рук в то же состояние
    void Update()
    {
            var state = sourceAnimator.GetCurrentAnimatorStateInfo(0);
            myAnimator.Play(state.fullPathHash, 0, state.normalizedTime);
    }
}