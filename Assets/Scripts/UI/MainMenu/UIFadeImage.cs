using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeImage : MonoBehaviour
{
    [Header("Fade Variables")]

    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float fadeTime;

    public void StartFade(GameEvent myEvent)
    {
        StartCoroutine(Fade(myEvent));
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator Fade(GameEvent myEvent)
    {
        float alphaValue;

        while (fadeImage.color.a != 1)
        {
            yield return new WaitForFixedUpdate();

            alphaValue = fadeImage.color.a;
            alphaValue = Mathf.MoveTowards(alphaValue, 1, (1 / fadeTime) * Time.deltaTime);

            fadeImage.color = new Color(0, 0, 0, alphaValue);
        }

        myEvent.Raise();
    }

    private IEnumerator FadeOut()
    {
        float alphaValue;

        yield return new WaitForSeconds(fadeTime);

        while (fadeImage.color.a != 0)
        {
            yield return new WaitForFixedUpdate();

            alphaValue = fadeImage.color.a;
            alphaValue = Mathf.MoveTowards(alphaValue, 0, (1 / fadeTime) * Time.deltaTime);

            fadeImage.color = new Color(0, 0, 0, alphaValue);
        }
    }
}
