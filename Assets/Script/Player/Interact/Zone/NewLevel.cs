using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel:MonoBehaviour
{
    [SerializeField]
    private string Scene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModule>())
        {
            SceneManager.LoadScene(Scene);
        }
    }
}