using System.Collections;
using System.Threading;
using UnityEngine;

// Класс, отвечающий за лечение объектов с классом Health при соприкосновении коллизий.
public class HealingZone : MonoBehaviour
{
    //Объем лечения
    [SerializeField] private int healAmount = 10;
    // Интервал лечения
    [SerializeField] private float healInterval = 1f;
    // Корутина
    private Coroutine healingCoroutine;

    // Ищет компонент Health у объекта и если находит - начинает корутину.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Health health = collider.GetComponent<Health>();
        if (health != null)
        {
            healingCoroutine = StartCoroutine(HealOverTime(health));
        }
    }

    // Остановка корутины при выходе из области лечения.
    private void OnTriggerExit2D()
    {
        if (healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
        }
    }

    // Цикл, лечащий все объекты пока корутина действительная. Получает параметры объема и интервала лечения.
    private IEnumerator HealOverTime(Health health)
    {
        while (true)
        {
            health.Heal(healAmount);
            yield return new WaitForSeconds(healInterval);
        }
    }
}
