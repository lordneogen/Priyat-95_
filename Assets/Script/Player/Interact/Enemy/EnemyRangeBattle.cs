using System;
using UnityEngine;

public class EnemyRangeBattle:MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>()==null) return;
        // EventManager.Instance.Fade.FadeIn();
        EventManager.Instance.RPGfight.Enable(_enemy);
    }
}