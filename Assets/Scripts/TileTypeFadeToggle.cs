using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class TilemapFade : MonoBehaviour
{
    [SerializeField] private TilemapRenderer tilemapRenderer; 
    [SerializeField] private float fadeDuration = 1f;

    private bool isVisible = true;
    private Coroutine fadeRoutine;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // можно заменить проверку хоть на компонент PlayerMovement
        if (other.CompareTag("Player"))
            Toggle();
    }

    private void Toggle()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Fade(!isVisible));
    }

    private IEnumerator Fade(bool makeVisible)
    {
        Color startColor = tilemapRenderer.material.color;
        Color targetColor = startColor;
        targetColor.a = makeVisible ? 1f : 0f;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            tilemapRenderer.material.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        tilemapRenderer.material.color = targetColor;
        isVisible = makeVisible;
        fadeRoutine = null;
    }
}