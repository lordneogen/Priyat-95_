using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory:MonoBehaviour
{
    public GameObject LootUICeil;
    public GameObject Parent;
    public GameObject GUI;
    public List<GameObject> Ceils;
    private NewControls _playerControl;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Des;
    [HideInInspector] public InventoryCeil SelectCeil;

    private void Start()
    {
        EventManager.Instance.inventory = this;
    }

    private void OnEnable()
    {
        _playerControl = new NewControls();
        _playerControl.Player.Use.performed += ctx => Enable();
        _playerControl.Player.Exit.performed += ctx => Disable();
        // _playerInput.Player.Use.performed += ctx => Use();
        _playerControl.Enable();
        GUI.SetActive(false);
    }

    private void OnDisable()
    {
        _playerControl.Player.Use.performed -= ctx => Enable();
        _playerControl.Player.Exit.performed -= ctx => Disable();
        _playerControl.Disable();
    }
    public void Enable()
    {
        if(EventManager.Instance._Stopmove&&!GUI.activeSelf) return;
        EventManager.Instance._Stopmove = true;
        if (GUI.activeSelf)
        {
            Disable();
            return;
        }
        GUI.SetActive(true);
        Ceils = new List<GameObject>();
        List<ChessCeil> chessCeils = EventManager.Instance.Player.GetComponent<PlayerModule>().Characteers.Loots;
        foreach (var x in chessCeils)
        {
            GameObject ceil=Instantiate(LootUICeil, Parent.transform);
            Ceils.Add(ceil);
            InventoryCeil lootUICeil=ceil.GetComponent<InventoryCeil>();
            lootUICeil.SetLoot(x);
        }
    }

    public void Disable()
    {
        if(EventManager.Instance.PersonsSelectUI.gameObject.activeSelf) return;
        EventManager.Instance._Stopmove = false;
        foreach (var x in Ceils)
        {
            Destroy(x);
        }
        Ceils = new List<GameObject>();
        GUI.SetActive(false);
    }

    public void Restart()
    {
        foreach (var x in Ceils)
        {
            Destroy(x);
        }
        Ceils = new List<GameObject>();
        List<ChessCeil> chessCeils = EventManager.Instance.Player.GetComponent<PlayerModule>().Characteers.Loots;
        foreach (var x in chessCeils)
        {
            GameObject ceil=Instantiate(LootUICeil, Parent.transform);
            Ceils.Add(ceil);
            InventoryCeil lootUICeil=ceil.GetComponent<InventoryCeil>();
            lootUICeil.SetLoot(x);
        }
    }
}