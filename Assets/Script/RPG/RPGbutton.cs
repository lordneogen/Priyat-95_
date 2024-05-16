using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RPGbutton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _textField;
    private Reception _reception;

    public void Initializate(string text, Reception reception)
    {
        _reception = reception;
        _textField.SetText(text);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.Instance.RPGfight.RPGfightText.SetText($"{_reception.name} отнимает у врага {_reception.HP} HP и отнимает у вас {_reception.MP} MP");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.Instance.RPGfight.RPGfightText.SetText("Битва продолжается!");
    }
}