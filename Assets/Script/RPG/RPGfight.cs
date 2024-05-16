using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RPGfight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    public GameObject UIGameOver;
    private Enemy _enemy;
    public int EnemyHP;
    public int EnemyHPMax;
    public TextMeshProUGUI EnemyHPText;
    private Characteers _characteers;
    public RPGEnemyShake RpgEnemyShake;

    [SerializeField] private RectTransform EnemyHPBar;
    //
    public Transform PersonParent;
    public GameObject PersonPref;
    [SerializeField]
    private Image EnemyIcon;
    //
    public List<GameObject> Persons;
    public ScreenShake ScreenShake;
    //
    [HideInInspector]
    public int size;
    public int currSize;

    public RPGfight_text RPGfightText;
    
    //
    private NewControls _playerInputs;
    private List<string> textFight;

    public int deathIndex = 0;
    private int currIndex = 0;
    public TextMeshProUGUI EnemyName;
    //
    private void Awake()
    {
        _playerInputs = new NewControls();
        textFight = new List<string>();
    }
    void Start()
    {
        EventManager.Instance.RPGfight = this;
        UIGameOver.SetActive(false);
        UI.SetActive(false);
    }
    
    private void OnEnable()
    {
        _playerInputs.Player.Enable();
        _playerInputs.Player.Menu.performed += ctx => Next();
    }

    private void OnDisable()
    {
        _playerInputs.Player.Disable();
    }

    public void Enable(Enemy enemy)
    {
        EnemyIcon.sprite = enemy.Icon;
        _playerInputs.Player.Enable();
        _enemy = enemy;
        EnemyHP = _enemy.HP;
        EnemyHPMax = EnemyHP;
        EnemyName.SetText(_enemy.name);
        _characteers = EventManager.Instance.Player.GetComponent<Player>()._playerModule.Characteers;
        size = _characteers.Characters.Count;
        //
        UpdateHP();
        
        // EnemyPref = _enemy.FightModel;
        for (int i = 0; i < _characteers.Characters.Count; i++)
        {
            GameObject person = Instantiate(PersonPref,PersonParent);
            Persons.Add(person);
            RPGfight_person rpGfightPerson=person.GetComponent<RPGfight_person>();
            rpGfightPerson.Initialize(_characteers.Characters[i],this);
        }
        //
        currSize = size;
        UI.SetActive(true);
        RPGfightText.SetText("Битва началась!");
    }

    public void Next()
    { 
        RPGfightText.Premium = false;
            
        foreach (var variaPerson in Persons) 
        {
                variaPerson.GetComponent<RPGfight_person>().Restart();
        }

        currIndex = 0;
        textFight = new List<string>();
        currSize = size;
        if (textFight.Count == 0)
        {
            return;
        }
        currIndex++;
    }
    
    public void Disable()
    {
        for (int i = 0; i < _characteers.Characters.Count; i++)
        {
            Destroy(Persons[i]);
        }

        Persons = new List<GameObject>();
        //
        _enemy.transform.gameObject.SetActive(false);
        // EventManager.Instance._Stopmove = false;
        // EventManager.Instance.Fade.FadeOut();
        List<Dialog> dialogs = new List<Dialog>();
        foreach (var i in _enemy._enemyModule.Loots)
        {
            EventManager.Instance.Player.GetComponent<PlayerModule>().Characteers.Add(i);
            dialogs.Add(new Dialog($"Вы получили {i.Loot.name} в количестве {i.size}."));
        }
        EventManager.Instance.TextDialog.StartDialog(dialogs);
        UI.SetActive(false);
    }

    public void UpdateHP()
    {
        EnemyHPText.SetText(EnemyHP.ToString());
        EnemyHPBar.DOScaleX((float)EnemyHP / (float)EnemyHPMax, 0.5f);
    }

    public void EnemyHit()
    {
        Debug.Log("Enemy");
        string res = "";
        for (int i = 0; i < Persons.Count; i++)
        {
            RPGfight_person rpGfightPerson=Persons[i].GetComponent<RPGfight_person>();
            int AttHP = _enemy.Spells[Random.Range(0, _enemy.Spells.Count)].HP;
            rpGfightPerson.GetHit(AttHP);
            textFight.Add($"{Persons[i].GetComponent<RPGfight_person>().PersonName.text} HP {AttHP}");
            // Debug.Log($"{Persons[i].GetComponent<RPGfight_person>().PersonName} HP {AttHP}");
        }
    }
    //
    // public void BackDamage()
    // {
    //     int pr=Random.Range(0, 2);
    //     if (pr == 0) person1HP -= 10;
    //     if (pr == 1) person2HP -= 10;
    //     RectTransform1.DOScale(new Vector3((float)person1HP / 100,1,1),0.5f);
    //     RectTransform2.DOScale( new Vector3((float)person2HP / 100,1,1),0.5f);
    //     person = 2;
    // }
}
