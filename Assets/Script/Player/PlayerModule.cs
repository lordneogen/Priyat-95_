using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public class LootData
{
    public string name;
    public int size;

    public LootData(string name,int size)
    {
        this.name = name;
        this.size = size;
    }
}

[Serializable]

public class SkillData
{
    public string name;

    public SkillData(string name)
    {
        this.name = name;
    }
}

[Serializable]
public class CharacterData
{
    public int HP;
    public int MP;
    public int LV;
    public string name;
    public List<SkillData> SkillDatas;

    public CharacterData(Character character)
    {
        HP = character.HP;
        MP = character.MP;
        LV = character.LV;
        name = character.CharacterSingle.name;
        SkillDatas = new System.Collections.Generic.List<SkillData>();
        foreach (var x in character.Receptions)
        {
            SkillDatas.Add(new SkillData(x.name));
        }
    }
}


[Serializable]
public class PlayerData
{
    public List<float> pos;
    public List<CharacterData> CharacterDatas;
    public List<LootData> LootDatas;
    // public int SceneNum;

    public PlayerData(Characteers characteers)
    {
        CharacterDatas = new List<CharacterData>();
        LootDatas = new List<LootData>();
        foreach (var x in characteers.Characters)
        {
            CharacterDatas.Add(new CharacterData(x));
        }
        foreach (var x in characteers.Loots)
        {
            Debug.Log(2);
            LootDatas.Add(new LootData(x.Loot.name,x.size));
        }
    }
    public Characteers Characteers()
    {
        return new Characteers(CharacterDatas,LootDatas);
    }
}

public class PlayerModule : MonoBehaviour
{
    public float speed = 5;
    public float jumpSpeed = 8;
    public float gravity = 9.8f;
    public float vSpeed = 0;
    private CharacterController _controller;
    private Player _player;
    private NewControls _playerInput;
    private IIntercact _intercact;
    [SerializeField] private string SaveName;
    //
    public Characteers Characteers;
    //
    private void OnEnable()
    {
        _playerInput.Player.Enable();
        _playerInput.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        _playerInput.Player.Use.performed += ctx => Use();
    }
    //
    private void OnDestroy()
    {
        _playerInput.Player.Move.performed -= ctx => Move(ctx.ReadValue<Vector2>());
        _playerInput.Player.Use.performed -= ctx => Use();
        _playerInput.Player.Disable();
    }
    //
    private void Awake()
    {
        _playerInput = new NewControls();
        _player = GetComponent<Player>();
        _controller = GetComponent<CharacterController>();
        // LoadPlayerData();
    }
    //
    private void FixedUpdate()
    {
        Move(_playerInput.Player.Move.ReadValue<Vector2>());
    }

    //
    private void Move(Vector2 vector2)
    {
        Vector3 InputVel = new Vector3(vector2.x, 0, vector2.y);
        if(EventManager.Instance._Stopmove||EventManager.Instance.RPGfight.UI.activeSelf||EventManager.Instance.TextDialog.GUI.activeSelf) InputVel=Vector3.zero;
        //
        if (EventManager.Instance._Stopmove) InputVel = Vector3.zero;
        //
        InputVel.Normalize();
        InputVel *= speed;
        //
        if (_controller.isGrounded)
        {
            vSpeed = 0;
            //
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
                _player._playerView.SetAnim("jump", 0.001f);
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                InputVel *= 2;
                _player._playerView.SetAnim("run");
            }
            //
            if (Math.Abs(InputVel.x) > 0 || Math.Abs(InputVel.z) > 0) _player._playerView.SetAnim("walk");
            else _player._playerView.SetAnim("idle");
        }
        if (InputVel != Vector3.zero)
        {
            _player._playerView.SetRotation(InputVel);
        }
        vSpeed -= gravity * Time.deltaTime;
        InputVel.y = vSpeed;
        //
        _controller.Move(InputVel * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IIntercact>() != null) _intercact = other.GetComponent<IIntercact>();
    }

    private void OnTriggerExit(Collider other)
    {
        _intercact = null;
    }

    // public void SwitchCharacter(CharacterSingle characterSingle)
    // {
    //     _player._playerView.model = characterSingle.model;
    // }

    private void Use()
    {
        if (_intercact == null) return;
        _intercact.Interact();
    }
    
    //
    
    public void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/{SaveName}.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerPrefs.SetInt("SceneNum",SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        PlayerData data = new PlayerData(Characteers);
        data.pos = new List<float>(){transform.position.x,transform.position.y,transform.position.z};
        // data.SceneNum=SceneManager.GetActiveScene().buildIndex;
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Player data saved!");
    }

    public void LoadPlayerData()
    {
        int SceneInt=PlayerPrefs.GetInt("SceneNum");
        if (SceneInt != null && SceneInt!=SceneManager.GetActiveScene().buildIndex && SceneInt!=0)
        {
            SceneManager.LoadScene(SceneInt);
        }
        string path = Application.persistentDataPath + $"/{SaveName}.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Vector3 position = new Vector3(data.pos[0],data.pos[1],data.pos[2]);
            Characteers = data.Characteers();
            _controller.enabled = false; // Disable the controller before changing the position
            transform.position = position;
            _controller.enabled = true; // Re-enable the controller after changing the position
            Debug.Log("Player data loaded!");
        }
        else
        {
            Debug.Log("No saved data found.");
        }
    }
}
