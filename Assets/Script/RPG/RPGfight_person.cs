using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RPGfight_person:MonoBehaviour
{
    public RectTransform HPbar;
    public RectTransform MPbar;
    public TextMeshProUGUI PersonName;
    public TextMeshProUGUI HPint;
    public TextMeshProUGUI MPint;
    //
    [SerializeField]
    private Image Icon;
    [FormerlySerializedAs("parentTranform")] [SerializeField] public Transform parentButtonTranform;
    //
    public GameObject _button;
    private List<GameObject> Buttons;
    private Character _character;
    //
    public RPGfight RPGfight;
    //
    public bool use;
    public bool death;

    public void Initialize(Character characterSingle,RPGfight rpGfight)
    {
        RPGfight = rpGfight;
        _character = characterSingle;
        Buttons = new List<GameObject>();
        Icon.sprite = characterSingle.CharacterSingle.Icon;
        PersonName.SetText(characterSingle.CharacterSingle.name);
        // _image.sprite = characterSingle.CharacterSingle.Icon;
        //
        for (int i = 0; i < _character.Receptions.Count; i++)
        {
            int index = i; // Создаем локальную переменную, чтобы зафиксировать значение i
            GameObject button = Instantiate(_button,parentButtonTranform);
            Buttons.Add(button);
            RPGbutton rpGbutton = button.GetComponent<RPGbutton>();
            Button rpGbuttonbutton = button.GetComponent<Button>();
            rpGbutton.Initializate(_character.Receptions[index].name,_character.Receptions[index]);
            rpGbuttonbutton.onClick.AddListener(delegate { OnClick(_character.Receptions[index]); });
        }
        //
        PersonName.SetText(characterSingle.CharacterSingle.name);
        UpdateScale();
    }

    public void UpdateScale()
    {
        HPint.SetText(_character.HP.ToString());
        MPint.SetText(_character.MP.ToString());
        HPbar.DOScaleX((float)_character.HP / _character.CharacterSingle.HP, 0.5f);
        MPbar.DOScaleX((float)_character.MP / _character.CharacterSingle.MP, 0.5f);
    }

    public void Restart()
    {
        if (death)
        {
            return;
        }
        use = false;
        foreach (var button in Buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }

    public void OnClick(Reception spell)
    {
        if(use||_character.HP < 0||_character.MP+spell.MP<0) return;
        EventManager.Instance.RPGfight.RpgEnemyShake.Shake();
        foreach (var button in Buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }
        // RPGfight.size--;
        use = true;
        RPGfight.EnemyHP += spell.HP;
        _character.MP += spell.MP;
        //
        UpdateScale();
        //
        if (RPGfight.EnemyHP <= 0)
        {
            RPGfight.Disable();
        }
        //
        RPGfight.currSize--;
        
        RPGfight.UpdateHP();
        if(RPGfight.currSize-RPGfight.deathIndex==0) RPGfight.EnemyHit();
        return;
    }

    public void GetHit(int _hp)
    {
        _character.HP += _hp;
        _character.HP = Mathf.Max(_character.HP, 0);
        UpdateScale();
        if (_character.HP <= 0 && !death)
        {
            RPGfight.deathIndex++;
            death = true;
        }
        if (RPGfight.size <= RPGfight.deathIndex)
        {
            RPGfight.UIGameOver.SetActive(true);
            Debug.Log("Death");
        }
    }

    public void Start()
    {
        // Sequence sequence = DOTween.Sequence();
        // sequence.Append(transform.DOMoveY(0.001f,0.5f));
        // sequence.Append(transform.DOMoveY(-0.001f,0.5f));
        // sequence.SetLoops(-1);
    }
}
