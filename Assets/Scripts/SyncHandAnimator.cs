using UnityEngine;

 // Класс, отвечающий за синхронизацию аниматора рук на основе тела игрока.
public class SyncHandAnimator : MonoBehaviour
{
    // Аниматор тела
    public Animator sourceAnimator;
    // Аниматор рук
    private Animator myAnimator;

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