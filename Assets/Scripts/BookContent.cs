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
            SetConversion(dropdown.value);
            ChangeChapter(chapter);
        }
        private void UpdateText(string text)
        {
            readingBoxText.text = ConvertText(text);
        }
        private string ConvertText(string text)
        {
            string converted = "";
            string word1 = "";
            string word2 = "";
            string word3 = "";
            string word4 = "";
            string word5 = "";
            string word6 = "";
            string word7 = "";
            switch (conversion)
            {
                case 0:
                    {
                        word1 = "alcoholics";
                        word2 = "alcoholic";
                        word3 = "alcoholism";
                        word4 = "Alcoholics Anonymous";
                        word5 = "drinking";
                        word6 = "alcohol";
                        word7 = "drinkers";

                    }

                    break;
                case 1:
                    {
                        word1 = "sex addicts";
                        word2 = "sex addict";
                        word3 = "sex addiction";
                        word4 = "SAA";
                        word5 = "acting out";
                        word6 = "selfish sex";
                        word7 = "addicts";
                    }

                    break;

            }
            converted = text.Replace("$alcoholics", word1);
            converted = converted.Replace("$alcoholic", word2);
            converted = converted.Replace("$alcoholism", word3);
            converted = converted.Replace("$Alcoholics-Anonymous", word4);
            converted = converted.Replace("$drinking", word5);
            converted = converted.Replace("$drinking", word6);
            converted = converted.Replace("$drinking", word7);

            return converted;
        }
        public void SetConversion(int conv)
        {
            conversion = conv;
        }
        public void ChangeChapter(int num)
        {
            chapter = num;

            int bottomLength = -10000;
            switch (chapter)
            {
                case 0:
                    bottomLength = -11500;
                    break;
                case 1:
                    bottomLength = -13000;
                    break;
                case 2:
                    bottomLength = -14500;
                    break;
                case 3:
                    bottomLength = -14000;
                    break;
                case 4:
                    bottomLength = -14500;
                    break;
                case 5:
                    bottomLength = -13000;
                    break;
            }
            readingBoxText.rectTransform.SetBottom(bottomLength);
            UpdateText(loadingScript.chapters[chapter]);
           
        }
    }
}
     