using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Health playerHealthScript;

    // Update is called once per frame
    void Update()
    {
        float current = Mathf.Clamp(playerHealthScript.CurrentHealth, 0, playerHealthScript.MaxHealth);
        float max = playerHealthScript.MaxHealth;
        healthText.text = $"{current}/{max}";
    }
}
