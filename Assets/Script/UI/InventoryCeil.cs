using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCeil : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI Name;
    private string des;
    public TextMeshProUGUI Count;
    [SerializeField] private Color SelectColor;
    [SerializeField] private Color UnSelectColor;
    [SerializeField] private Image Fone;
    [HideInInspector] public ChessCeil _chessCeil;

    public void SetLoot(ChessCeil chessCeil)
    {
        des = chessCeil.Loot.des;
        _chessCeil = chessCeil;
        Name.SetText(chessCeil.Loot.name);
        Count.SetText(chessCeil.size.ToString());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.Instance.inventory.Name.SetText(Name.text);
        EventManager.Instance.inventory.Des.SetText(des);
        Fone.color = SelectColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Fone.color = UnSelectColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_chessCeil.Loot.HP==0&&_chessCeil.Loot.MP==0) return;
        EventManager.Instance.inventory.SelectCeil = this;
        EventManager.Instance.PersonsSelectUI.gameObject.SetActive(!EventManager.Instance.PersonsSelectUI.gameObject.activeSelf);
    }
}