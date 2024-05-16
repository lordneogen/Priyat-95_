using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Loot", menuName = "Custom/Loot")]
public class Loot:ScriptableObject
{
    public string name = "";
    public string des = "";
    public int HP = 0;
    public int MP = 0;
}