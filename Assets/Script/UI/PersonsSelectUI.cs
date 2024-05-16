using System;
using System.Collections.Generic;
using UnityEngine;

public class PersonsSelectUI:MonoBehaviour
{
    public GameObject PersonSingl;
    public Transform ParentTranform;
    private List<GameObject> Persons;
    private NewControls _playerControl;

    private void Start()
    {
        Persons = new List<GameObject>();
        _playerControl = new NewControls();
        // _playerControl.Player.Use.performed += ctx => Enable();
        _playerControl.Player.Exit.performed += ctx => gameObject.SetActive(false);
        _playerControl.Player.Enable();
        EventManager.Instance.PersonsSelectUI = this;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // gameObject.SetActive(true);
        if(EventManager.Instance==null||EventManager.Instance.Player==null) return;
        Characteers characteers = EventManager.Instance.Player.GetComponent<PlayerModule>().Characteers;
        Debug.Log(characteers.Characters.Count);
        foreach (var x in characteers.Characters)
        {
            Debug.Log(x.CharacterSingle.name);
            GameObject Personitem = Instantiate(PersonSingl,ParentTranform);
            Personitem.GetComponent<PersonSelect>().Create(x);
            Persons.Add(Personitem);
        }
    }

    private void OnDisable()
    {
        // _playerControl.Player.Disable();
        Debug.Log(Persons);
        if(Persons==null) return;
        foreach (var x in Persons)
        {
            Destroy(x);
        }
    }

    private void OnDestroy()
    {
        if(_playerControl==null) return;
        _playerControl.Player.Disable();
    }
}