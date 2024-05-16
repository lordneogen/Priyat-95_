using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Reception", menuName = "Custom/Reception")]
public class Reception:ScriptableObject
{
    public string name = "";
    public string des = "";
    //
    public int MPdown = 0;
    //
    public int HP = 0;
    public int MP = 0;
    //
    public List<Reception> NextReceptions;
}