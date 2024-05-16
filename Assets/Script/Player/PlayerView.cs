using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Animator Animator;
    public List<string> TagAnim;
    public GameObject model;
    public float AnimSpeed=1f;

    public void SetAnim(string tag,float waitTime=0.1f)
    {
        foreach (var i in TagAnim)
        {
            if (i != tag)
            {
                Animator.SetFloat(i, 0f, waitTime, Time.deltaTime);
            }
        }
        Animator.SetFloat(tag, 1f, waitTime, Time.deltaTime);
    }

    public void SetRotation(Vector3 vector3)
    {
        model.transform.DORotate(Quaternion.LookRotation(new Vector3(vector3.x,0,vector3.z)).eulerAngles,AnimSpeed);
    }
    //
    // public void ChangeCharacter(CharacterSingle characterSingle)
    // {
    //     GameObject gameObject=Instantiate(characterSingle.model,transform);
    //     gameObject.GetComponent<Animator>().avatar=
    // }
}
