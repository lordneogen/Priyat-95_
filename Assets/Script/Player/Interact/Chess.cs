using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ChessCeil
{
    public Loot Loot;
    public int size;
    public ChessCeil(Loot loot,int size)
    {
        Loot = loot;
        this.size = size;
    }
}

[Serializable]
public class Used
{
    public bool used;
}

public class Chess:MonoBehaviour, IIntercact
{
    public List<ChessCeil> Loots;
    public bool used = false;

    [SerializeField]
    // private TextMeshProUGUI TextMeshProUGUI;
    public List<Dialog> StartDialog;
    public List<Dialog> Texts;

    private void Awake()
    {
        Load();
    }

    private void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/{transform.position.x}{transform.position.y}{transform.position.z}.save";
        FileStream stream = new FileStream(path, FileMode.Create); 
        Used data = new Used();
        data.used = true;
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    private void Load()
    {
        string path = Application.persistentDataPath + $"/{transform.position.x}{transform.position.y}{transform.position.z}.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Used data = formatter.Deserialize(stream) as Used;
            stream.Close();
            used = data.used;
        }
        else
        {
            Debug.Log("No saved data found.");
        }
    }

    public void Interact()
    {
        if (used)
        {
            Texts = new List<Dialog>();
            Texts.Add(new Dialog($"Здесь ничего нет"));
            EventManager.Instance.TextDialog.StartDialog(Texts);
            return;
        }
        used = true;
        Texts = new List<Dialog>();
        foreach (var i in Loots)
        {
            EventManager.Instance.Player.GetComponent<PlayerModule>().Characteers.Add(i);
            Texts.Add(new Dialog($"Вы получили {i.Loot.name} в количестве {i.size} штук."));
        }
        StartDialog.AddRange(Texts);
        EventManager.Instance.TextDialog.StartDialog(StartDialog);
        Save();
    }
}