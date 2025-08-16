using System.Collections;
using System.Threading;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;
    [SerializeField] private float healInterval = 1f;
    private Coroutine healingCoroutine;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Health health = collider.GetComponent<Health>();
        if (health != null)
        {
            healingCoroutine = StartCoroutine(HealOverTime(health));
        }
    }

    private void OnTriggerExit2D()
    {
        if (healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
        }
    }

    private IEnumerator HealOverTime(Health health)
    {
        while (true)
        {
            health.Heal(healAmount);
            yield return new WaitForSeconds(healInterval);
        }
    }
}
