using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public string name;
    public int HP;
    public int MP;
    // public GameObject FightModel;
    public List<Reception> Spells;
    public Sprite Icon;
    //
    [HideInInspector]
    public EnemyModule _enemyModule;
    [HideInInspector]
    public EnemyView _enemyView;
    private void Awake()
    {
        _enemyView = GetComponent<EnemyView>();
        _enemyModule = GetComponent<EnemyModule>();
    }
}
