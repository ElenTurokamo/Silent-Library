using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
    {
        Debug.Log("Удар по " + collider.name); // проверка срабатывания
        Health health = collider.GetComponent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }
    }
    }
}
