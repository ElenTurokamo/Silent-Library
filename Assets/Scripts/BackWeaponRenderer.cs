using UnityEngine;

public class BackWeaponRenderer : MonoBehaviour
{
    public GameObject Character;
    public GameObject weaponSprite;

    [Header("Positions")]
    public Vector3 positionUp;
    public Vector3 positionDown;
    public Vector3 positionLeft;
    public Vector3 positionRight;

    private Animator animator;
    
    void Start()
    {
        if (Character != null)
        {
            animator = Character.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animator == null || weaponSprite == null) return;

        bool isAttacking = animator.GetBool("isAttacking");
        weaponSprite.SetActive(!isAttacking);

        if (isAttacking) return;

        float moveX = animator.GetFloat("Horizontal");
        float moveY = animator.GetFloat("Vertical");

        if (moveY > 0.5f)
        {
            transform.localPosition = positionUp;
        }  
        else if (moveY < -0.5f)
        {
            transform.localPosition = positionDown;
        }
        else if (moveX > 0.5f)
        {
            transform.localPosition = positionRight;
        }
        else if (moveX < -0.5f)
        {
            transform.localPosition = positionLeft;
        } 
    }
}
