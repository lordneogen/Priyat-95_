using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PersonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Char;
    public Image Icon;
    [SerializeField] private Color SelectColor;
    [SerializeField] private Color UnSelectColor;
    [SerializeField] private Image Fone;
    private Character thisChar;
    public void Create(Character character)
    {
        thisChar = character;
        Name.SetText(character.CharacterSingle.name);
        Char.SetText($"{character.HP}\n" +
                     $"{character.MP}\n" +
                     $"{character.LV}");
        Icon.sprite = character.CharacterSingle.Icon;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Fone.color = SelectColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Fone.color = UnSelectColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (thisChar.HP == 0&&EventManager.Instance.RPGfight.UI.activeSelf) return;
        thisChar.HP += EventManager.Instance.inventory.SelectCeil._chessCeil.Loot.HP;
        thisChar.MP += EventManager.Instance.inventory.SelectCeil._chessCeil.Loot.MP;
        thisChar.HP = Mathf.Min(thisChar.HP, thisChar.CharacterSingle.HP);
        thisChar.MP = Mathf.Min(thisChar.MP, thisChar.CharacterSingle.MP);
        EventManager.Instance.Player.GetComponent<PlayerModule>().Characteers.Remove(EventManager.Instance.inventory.SelectCeil._chessCeil);
        EventManager.Instance.inventory.Restart();
        foreach (var x in EventManager.Instance.RPGfight.Persons)
        {
            x.GetComponent<RPGfight_person>().UpdateScale();
        }
        EventManager.Instance.PersonsSelectUI.gameObject.SetActive(!EventManager.Instance.PersonsSelectUI.gameObject.activeSelf);
    }
}
