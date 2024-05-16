using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC:MonoBehaviour, IIntercact
{
    [SerializeField]
    private string name;
    private TextMeshProUGUI TextMeshProUGUI;
    public List<Dialog> Texts;
    
    public void Interact()
    {
        EventManager.Instance.TextDialog.StartDialog(Texts,true,name);
    }
}