using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100;

    private float MAX_HEALTH;

    public Image healthBar;


    void Start()
    {
        MAX_HEALTH = health;
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / MAX_HEALTH, 0 , 1);
        if (Input.GetKeyDown(KeyCode.G))
        {
            Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;

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
        }
    }

    private void Die()
    {
        Debug.Log($"The {this} is dead");
        Destroy(gameObject);
    }
}
