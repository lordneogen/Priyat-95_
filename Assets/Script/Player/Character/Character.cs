using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    public CharacterSingle CharacterSingle;
    public List<Reception> Receptions;
    public int HP = 100;
    public int MP = 100;
    public int LV = 1;

    public Character(CharacterSingle characterSingle,List<Reception> receptions,int hp,int mp,int lv)
    {
        CharacterSingle = characterSingle;
        Receptions = receptions;
        HP = hp;
        MP = mp;
        LV = lv;
    }
    
    public void LVup()
    {
        LV++;
        HP += (int)Math.Pow(1.15f, LV)*10;
        MP += (int)Math.Pow(1.5f, LV)*10;
    }

    public void Init()
    {
        HP = CharacterSingle.HP;
        MP = CharacterSingle.MP;
        Receptions = CharacterSingle.Receptions;
        LV = 1;
    }

    public int _HPscoreChange(Reception reception=null,Loot loot=null)
    {
        if (reception != null) return reception.HP;
        if (loot != null) return loot.HP;
        return 0;
    }
    
    public int _MPscoreChange(Reception reception=null,Loot loot=null)
    {
        if (reception != null) return reception.MP;
        if (loot != null) return loot.MP;
        return 0;
    }
    
    public void _ChooseReception(int indexold,int indexnew)
    {
        Receptions[indexold] = Receptions[indexold].NextReceptions[indexnew];
    }
}