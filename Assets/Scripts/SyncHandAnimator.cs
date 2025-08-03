using UnityEngine;

public class SyncHandAnimator : MonoBehaviour
{
    public Animator sourceAnimator;
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        var state = sourceAnimator.GetCurrentAnimatorStateInfo(0);
        myAnimator.Play(state.fullPathHash, 0, state.normalizedTime);
    }
}