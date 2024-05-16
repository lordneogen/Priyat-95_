using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class EnemyModule : MonoBehaviour
{
    public float speed= 5;
    public float gravity= 9.8f;
    public float vSpeed = 0;
    public float timeChange = 1f;
    public float timeRepeat = 1.5f;
    private CharacterController _controller;
    private Enemy _enemy;
    [HideInInspector]
    public Vector2 _moveVector=new Vector2();
    [HideInInspector]
    public bool _inRange;
    public List<ChessCeil> Loots;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _controller = GetComponent<CharacterController>();
        InvokeRepeating("Vector2Change",0,timeChange);
    }

    private void Vector2Change()
    {
        if(!_inRange) _moveVector = new Vector2((float)Random.Range(-1f, 1f), (float)Random.Range(-1f, 1f))/2+_moveVector/2;
    }
    //
    private void FixedUpdate()
    {
        Move(_moveVector);
    }
    //
    private void Move(Vector2 vector2){
        Vector3 InputVel = new Vector3(vector2.x, 0, vector2.y);
        //
        if(EventManager.Instance._Stopmove||EventManager.Instance.RPGfight.UI.activeSelf||EventManager.Instance.TextDialog.GUI.activeSelf) InputVel=Vector3.zero;
        //
        InputVel.Normalize();
        InputVel *= speed;
        if (_inRange) InputVel *= 2;
        //
        if (_controller.isGrounded){
            vSpeed = 0;
            if (!_inRange&&(Math.Abs( InputVel.x ) > 0 || Math.Abs( InputVel.z ) > 0)) _enemy._enemyView.SetAnim("walk");
            else if ( _inRange&&(Math.Abs( InputVel.x ) > 0 || Math.Abs( InputVel.z ) > 0)) _enemy._enemyView.SetAnim("run");
            else _enemy._enemyView.SetAnim("idle");
        }
        if (InputVel != Vector3.zero)
        {
            _enemy._enemyView.SetRotation(InputVel);
        }
        vSpeed -= gravity * Time.deltaTime;
        InputVel.y = vSpeed;
        //
        _controller.Move(InputVel * Time.deltaTime);
    }
    

    // private void MoveToTrigger()
}
