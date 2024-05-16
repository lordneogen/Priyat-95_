using UnityEngine;
using DG.Tweening;

public class RPGEnemyShake : MonoBehaviour
{
    // Duration of the shake effect
    public float duration = 1f;

    // Strength of the shake (amplitude)
    public float strength = 1f;

    // Vibrato (how much it will vibrate during the shake)
    public int vibrato = 10;

    // Randomness (how random the shake will be)
    public float randomness = 90f;

    // If you want to fade out the shake over time
    public bool fadeOut = true;

    public void Shake()
    {
        // Applying the shake effect to the GameObject's position
        transform.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
    }
}