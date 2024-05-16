using System.Collections.Generic;
using UnityEngine;

public class SavePoint:MonoBehaviour,IIntercact
    {
        public void Interact()
        {
            EventManager.Instance.Player.GetComponent<PlayerModule>().SavePlayerData();
            EventManager.Instance.TextDialog.StartDialog(new List<Dialog>(){new Dialog("Вы успешно сохранились.")});
        }
    }