using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu:MonoBehaviour
    {
        public void StartGame()
        {
            Debug.Log(12);
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void Exit()
        {
            EventManager.Instance._Stopmove = false;
            SceneManager.LoadScene(0);
        }
    }