using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public float delay = 1f;      // time before fade starts
    public float fadeDuration = 1f; // how long the fade takes

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(delay);

        float timer = 0f;
        Color c = sr.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            sr.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}