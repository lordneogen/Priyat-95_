using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeAmount = 0.7f;
    // Продолжительность тряски
    public float shakeDuration = 0.5f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0f;

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            // Трясем экран
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    // Метод для запуска тряски с указанной продолжительностью
    public void ShakeScreen(float duration)
    {
        originalPosition = transform.localPosition;
        shakeDuration = duration;
        currentShakeDuration = shakeDuration;
    }
}