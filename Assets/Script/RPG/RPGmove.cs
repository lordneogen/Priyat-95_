using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGmove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RPGmove = this;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
