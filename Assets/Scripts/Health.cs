using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] public GameObject HealVFX;

    private float MAX_HEALTH;
    public float lastHealTime = 0f;
    public Image healthBar;



    void Start()
    {
        MAX_HEALTH = health;
    }

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

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = Mathf.Clamp(health / MAX_HEALTH, 0, 1);
    }
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

    private void Die()
    {
        Debug.Log($"The {this} is dead");
        Destroy(gameObject);
    }

    public float CurrentHealth => health;
    public float MaxHealth => MAX_HEALTH;
}
