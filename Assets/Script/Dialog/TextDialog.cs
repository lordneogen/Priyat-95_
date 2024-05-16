using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Dialog
{
    public string text;
    public bool happy;
    public bool angry;
    public bool sad;
    public bool surprise;

    public Dialog(string text)
    {
        this.text = text;
    }
}

public class TextDialog:MonoBehaviour
{
    public GameObject GUI;
    public TextMeshProUGUI TextMeshProUGUI;
    private int indexCurrentText = 0;
    private int indexArrayText = 0;
    // private List<string> Texts=null;
    private List<Dialog> Dialogs;
    public bool Skip;
    private string currentText = "";
    private string endText = "";
    public float Wait;
    public Face Face;
    //
    private NewControls _playerInputs;
    //

    private void Awake()
    {
        _playerInputs = new NewControls();
    }

    private void Start()
    {
        GUI.SetActive(false);
        EventManager.Instance.TextDialog = this;
    }

    private void OnEnable()
    {
        _playerInputs.Player.Enable();
        _playerInputs.Player.Menu.performed += ctx => NextText();
    }

    private void OnDisable()
    {
        _playerInputs.Player.Disable();
    }

    public void StartDialog(List<Dialog> Texts,bool face_on,string name)
    {
        if(EventManager.Instance._Stopmove&&!GUI.activeSelf) return;
        GUI.SetActive(true);
        EventManager.Instance.FacesList.OffAll();
        if (face_on&&name!="null") this.Face = EventManager.Instance.FacesList.Select(name).Model.GetComponent<Face>();
        if(this.Dialogs!=null) return;
        indexArrayText = 0;
        Dialogs = Texts;
        EventManager.Instance._Stopmove = true;
        StartCoroutine(SetTextSingle(Texts[indexArrayText]));
    }
    
    public void StartDialog(List<Dialog> Texts)
    {
        GUI.SetActive(true);
        EventManager.Instance.FacesList.OffAll();
        // if (face_on&&name!="null") this.Face = EventManager.Instance.FacesList.Select(name).Model.GetComponent<Face>();
        if(this.Dialogs!=null) return;
        indexArrayText = 0;
        Dialogs = Texts;
        EventManager.Instance._Stopmove = true;
        StartCoroutine(SetTextSingle(Texts[indexArrayText]));
    }
    
    private IEnumerator SetTextSingle(Dialog text)
    {
        indexCurrentText = 0;
        currentText = "";
        endText = text.text;
        //
        try
        {
            Face.SetFace(text);
        }
        catch
        {
            
        }
        while (currentText.Length<endText.Length)
        {
            currentText += endText[indexCurrentText];
            indexCurrentText++;
            TextMeshProUGUI.SetText(currentText);
            yield return new WaitForSeconds(Wait);
        }
    }

    private void NextText()
    {
        StopAllCoroutines();
        if(Dialogs==null) return;
        indexArrayText++;
        if (indexArrayText >= Dialogs.Count) {
            //
            GUI.SetActive(false);
            Dialogs    = null;
            EventManager.Instance._Stopmove = false;
        }
        else StartCoroutine(SetTextSingle(Dialogs[indexArrayText]));
    }

    private void FixedUpdate()
    {
        
    }
}