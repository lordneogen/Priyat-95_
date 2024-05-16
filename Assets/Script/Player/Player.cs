using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public PlayerModule _playerModule;
    [HideInInspector]
    public PlayerView _playerView;
    private void Awake()
    {
        _playerView = GetComponent<PlayerView>();
        _playerModule = GetComponent<PlayerModule>();
    }
    private void Start()
    {
        EventManager.Instance.Player = gameObject;
    }
}
