using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class RPGfight_text:MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI Text;
        private string currentText = "";
        private string endText = "";
        public float Wait=0.1f;
        //
        public bool Premium=false;

        public void SetText(string text)
        {
            StopAllCoroutines();
            if(!Premium) StartCoroutine(SetTextSingle(text));
        }
        
        public void SetText(string text,bool premium)
        {
            Premium = premium;
            StopAllCoroutines();
            StartCoroutine(SetTextSingle(text));
        }
            
        private IEnumerator SetTextSingle(string text)
            {
                currentText = "";
                endText = text;
                int indexNum = 0;
                while (currentText.Length<endText.Length)
                {
                    currentText += endText[indexNum];
                    indexNum++;
                    Text.SetText(currentText);
                    yield return new WaitForSeconds(Wait);
                }
            }
    }