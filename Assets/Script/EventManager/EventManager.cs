using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class EventManager:MonoBehaviour
{
    [HideInInspector] public TextDialog TextDialog;
    [HideInInspector] public FacesList FacesList;
    [HideInInspector] public Fade Fade;
    [HideInInspector] public RPGmove RPGmove;
    [HideInInspector] public RPGfight RPGfight;
    [HideInInspector] public GameObject Player;
    [HideInInspector] public PersonsSelectUI PersonsSelectUI;
    [FormerlySerializedAs("LootUI")] [HideInInspector] public Inventory inventory;
    public static EventManager Instance=null;
    [HideInInspector]
    public bool _Stopmove;
    
    //
    void Awake () {
        //
        if (Instance == null) {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        //
    }

    private void Start()
    {
        Player.GetComponent<PlayerModule>().LoadPlayerData();
    }
}