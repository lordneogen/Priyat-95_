using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private void Awake()
    {
        image.gameObject.SetActive(true);
    }

    private void Start()
    {
        EventManager.Instance.Fade = this;
        FadeOut();
    }

    public void FadeIn(float duration = 1f, Action onComplete = null)
    {
        // Make the object transparent
        Color originalColor = image.color;
        Color transparentColor = originalColor;
        transparentColor.a = 1f;

        // Perform the fade in animation
        image.DOColor(transparentColor, duration).From(originalColor).OnComplete(() =>
        {
            // Invoke the onComplete action if provided
            onComplete?.Invoke();
        });
    }

    public void FadeOut(float duration = 1f, Action onComplete = null)
    {
        // Make the object transparent
        Color originalColor = image.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0f;

        // Perform the fade out animation
        image.DOColor(transparentColor, duration).From(originalColor).OnComplete(() =>
        {
            // Invoke the onComplete action if provided
            onComplete?.Invoke();
        });
    }
}