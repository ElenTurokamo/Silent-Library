using UnityEngine;

public class PlayerArmsController : MonoBehaviour
{
    [SerializeField] public Animator anim;
    public GetPlayerInputState inputState;

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if (inputState.IsMoving)
        {
            anim.SetFloat("X", inputState.X);
            anim.SetFloat("Y", inputState.Y);
        }

        anim.SetBool("Moving", inputState.IsMoving);
    }
}