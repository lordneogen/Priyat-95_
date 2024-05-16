using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "Custom/Character")]
public class CharacterSingle:ScriptableObject
{
    public string name="";
    public string des="";
    public Sprite Icon;
    public GameObject model=null;
    public int HP=100;
    public int MP=100;
    public List<Reception> Receptions;
}