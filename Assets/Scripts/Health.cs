using UnityEngine;
using UnityEngine.UI;

// Скрипт, отвечающий за наличие здоровья у игровых объектов.
public class Health : MonoBehaviour
{   
    // Кол-во ХП цели
    [SerializeField] private float health = 100;
    // Particle System, визуально показывающая лечение на объекте.
    [SerializeField] public GameObject HealVFX;

    // Максимальное здоровье.
    private float MAX_HEALTH;
    // Сколько времени прошло с последнего инстанса лечения.
    public float lastHealTime = 0f;
    // Элемент интерфейса, отображающий текущее здоровье.
    public Image healthBar;

    // На старте записывается максимальное здоровье объекта.
    void Start()
    {
        MAX_HEALTH = health;
    }

    // Временные вызовы методов лечения и получения урона через клавиши на клавиатуре.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }

    // Изменяет % заполнения полоски здоровья в зависимости от текущего хп.
    public void UpdateHealthBar()
    {
        healthBar.fillAmount = Mathf.Clamp(health / MAX_HEALTH, 0, 1);
    }

    // Метод, наносящий урон объекту.
    // Если здоровье заканчивается вызывается метод смерти.
    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    // Метод лечения. Лечит на определенное значение, если текущее здоровье не является максимальным.
    // Также создает эффект лечения на месте объекта, который лечится в данный момент.
    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
            Vector3 offset = new Vector3(0, 1f, 0);
            GameObject vfx = Instantiate(HealVFX, transform.position + offset, Quaternion.identity, transform);
            Destroy(vfx, 1f);
            UpdateHealthBar();
        }
    }

    // Метод смерти. Удаляет объект со сцены. (нужно добавить систему частиц для визуализации.)
    private void Die()
    {
        Debug.Log($"The {this} is dead");
        Destroy(gameObject);
    }

    // Публичная переменная, отображающая кол-во здоровье из приватной переменной.
    public float CurrentHealth => health;
    // Публичная переменная, отображающая кол-во максимального здоровья из приватной переменной.
    public float MaxHealth => MAX_HEALTH;
}
