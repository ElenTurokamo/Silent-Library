using UnityEngine;
using UnityEngine.UI;

public class HealthSlidebarUI : MonoBehaviour
{
    public Image frontBar;
    public Image backBar;

    public Health healthScript;
    public float lerpSpeed = 1f;

    // private float targetFill;

    void Start()
    {
        // targetFill = 1f;
        frontBar.fillAmount = 1f;
        backBar.fillAmount = 1f;

    }
    void Update()
    {
        float current = healthScript.CurrentHealth / healthScript.MaxHealth;

        frontBar.fillAmount = current;

        if (backBar.fillAmount > current)
        {
            backBar.fillAmount = Mathf.Lerp(backBar.fillAmount, current, Time.deltaTime * lerpSpeed);
        }
        else
        {
            backBar.fillAmount = current;
        }
    }
}
