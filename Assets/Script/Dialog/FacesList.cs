
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FacesOne
{
    public string name;
    public GameObject Model;

    public override string ToString()
    {
        return name;
    }
}

// [System.Serializable]
public class FacesList:MonoBehaviour
{
    public List<FacesOne> FFacesList;

    private void Start()
    {
        EventManager.Instance.FacesList = this;
        OffAll();
    }

    public FacesOne Select(string name)
    {
        foreach (var x in FFacesList)
        {
            if (x.ToString() == name)
            {
                x.Model.SetActive(true);
                return x;
            }
        }
        return null;
    }

    public void OffAll()
    {
        foreach (var x in FFacesList)
        {
            x.Model.SetActive(false);
        }
    }
}