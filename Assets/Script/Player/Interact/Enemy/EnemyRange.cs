using System;
using UnityEngine;

public class EnemyRange:MonoBehaviour
{
    [SerializeField]
    private EnemyModule _enemyModule;

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Player>()==null) return;
        _enemyModule._moveVector = - new Vector2(transform.position.x,transform.position.z) + new Vector2(other.transform.position.x,other.transform.position.z);
        _enemyModule._inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _enemyModule._inRange = false;
    }
}
