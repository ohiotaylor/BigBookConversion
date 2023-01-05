using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

namespace BookMechanics
{
    public class BookContent : MonoBehaviour
    {
        public GameObject readingBox;
        public InitialLoadingScript loadingScript;
        public TextMeshProUGUI readingBoxText;
        [SerializeField] private TMP_Dropdown dropdown;
        int conversion = 0;
        int chapter = 0;

        private void OnEnable()
        {
            //SetConversion(dropdown.value);
            //ChangeChapter(chapter);
            readingBoxText.text = loadingScript.chapters[0];
           
        }
        private void UpdateText(string text)
        {
            readingBoxText.text = text;
        }
        public void SetConversion(int conv)
        {
            conversion = conv;
        }
        public void ChangeChapter(int num)
        {
            chapter = num;

            int bottonLength = -10000;
            switch (chapter)
            {
                case 0:
                    bottonLength = -11500;
                    break;
                case 1:
                    bottonLength = -13000;
                    break;
                case 2:
                    bottonLength = -14500;
                    break;
                case 3:
                    bottonLength = -14000;
                    break;
                case 4:
                    bottonLength = -14500;
                    break;
                case 5:
                    bottonLength = -13000;
                    break;
            }
            readingBoxText.rectTransform.SetBottom(bottonLength);
            UpdateText(loadingScript.chapters[chapter]);
           
        }
    }
}
     