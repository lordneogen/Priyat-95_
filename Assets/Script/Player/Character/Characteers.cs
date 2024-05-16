using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Characteers
{
    public List<Character> Characters;
    public List<ChessCeil> Loots;

    public Characteers(List<CharacterData> characterDatas,List<LootData> lootDatas)
    {
        Characters = new List<Character>();
        Loots = new List<ChessCeil>();
        CharacterSingle[] CharRes = Resources.LoadAll<CharacterSingle>("Character");
        Loot[] LootRes = Resources.LoadAll<Loot>("Loot");
        Debug.Log(LootRes[0].name);
        Reception[] SkillRes = Resources.LoadAll<Reception>("Skill");
        //
        foreach (var x in characterDatas)
        {
            CharacterSingle characterSingle=new CharacterSingle();
            List<Reception> receptions=new List<Reception>();
            //
            foreach (var y in CharRes)
            {
                if (x.name == y.name)
                {
                    characterSingle = y;
                }
            }
            //
            foreach (var y in x.SkillDatas)
            {
                foreach (var z in SkillRes)
                {
                    if (y.name == z.name)
                    {
                        receptions.Add(z);
                    }
                }
            }
            Characters.Add(new Character(characterSingle,receptions,x.HP,x.MP,x.LV));
        }
        foreach (var x in lootDatas)
        {
            foreach (var y in LootRes)
            {
                if (x.name == y.name)
                {
                    Debug.Log(1);
                    Add(new ChessCeil(y,x.size));
                }
            }
        }
        return;
    }

    public PlayerData PlayerData()
    {
        return new PlayerData(this);
    }
    public void Add(ChessCeil ceil)
    {
        foreach (var i in Loots)
        {
            if (i.Loot.name == ceil.Loot.name)
            {
                i.size += ceil.size;
                return;
            }
        }
        Loots.Add(ceil);
    }
    
    public void Remove(ChessCeil ceil)
    {
        foreach (ChessCeil i in Loots)
        {
            if (i.Loot.name == ceil.Loot.name)
            {
                i.size--;
                if(i.size==0)
                {
                    Loots.Remove(i);
                }
                return;
            }
        }
    }
}