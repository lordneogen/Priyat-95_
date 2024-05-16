using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu:MonoBehaviour
{
    public GameObject Pause;
    public GameObject Settings;
    private NewControls _playerControl;
    [SerializeField] private bool MainMenu;
    private void Start()
    {
        if(MainMenu)EventManager.Instance = null;
        if(MainMenu) return;
        _playerControl = new NewControls();
        _playerControl.Player.Exit.performed += ctx => PauseOnOff();
        _playerControl.Enable();
        Pause.SetActive(false);
        Settings.SetActive(false);
    }

    private void OnDestroy()
    {
        if(MainMenu||_playerControl==null) return;
        _playerControl.Disable();
    }

    public void PauseOnOff()
    {
        if(MainMenu) return;
        if(!Pause.activeSelf)EventManager.Instance.Fade.FadeIn();
        else EventManager.Instance.Fade.FadeOut();
        Pause.SetActive(!Pause.activeSelf);
        Settings.SetActive(false);
        EventManager.Instance._Stopmove = Pause.activeSelf;
    }

    public void Restart()
    {
        EventManager.Instance.Player.GetComponent<PlayerModule>().LoadPlayerData();
        if(Pause.activeSelf)PauseOnOff();
        EventManager.Instance.RPGfight.UIGameOver.SetActive(false);
    }

    public void Setting()
    {
        Settings.SetActive(true);
    }
    
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