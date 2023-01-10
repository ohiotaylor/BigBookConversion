using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Newtonsoft.Json;

namespace BookMechanics
{
    public class InitialLoadingScript : MonoBehaviour
    {
        public string[] chapters;
        [SerializeField] private BookContentWithExamples Parser;
        BookContentWithExamples.EWordMapping fellowship;
        BookContentWithExamples.EWordMapping god;
        BookContentWithExamples.EWordMapping sex;
        [SerializeField] private TMP_Dropdown fellowshipDropdown;
        [SerializeField] private TMP_Dropdown godDropdown;
        [SerializeField] private TMP_Dropdown sexDropdown;
        [SerializeField] TextAsset bigBook;
        [SerializeField] TextAsset bigbook;
        [SerializeField] private TextAsset BBFull;

        private void OnEnable()
        {
            //bigBook = Resources.Load<TextAsset>("Book/BB.txt");
            LoadSettings();
            ReadTextFile();
            LoadJSON();
        }
        private void LoadSettings()
        {
            fellowshipDropdown.value = PlayerPrefs.GetInt("fellowship");
            godDropdown.value = PlayerPrefs.GetInt("god");
            sexDropdown.value = PlayerPrefs.GetInt("sex");
            ConvertDropdownValues();
            
        }
        private void ConvertDropdownValues()
        {
            switch (fellowshipDropdown.value)
            {
                case 0:
                    fellowship = BookContentWithExamples.EWordMapping.Alcohol;
                    break;
                case 1:
                    fellowship = BookContentWithExamples.EWordMapping.SAA;
                    break;
                default:
                    fellowship = BookContentWithExamples.EWordMapping.Alcohol;
                    PlayerPrefs.SetInt("fellowship", fellowshipDropdown.value);
                    break;
            }
            switch (godDropdown.value)
            {
                case 0:
                    god = BookContentWithExamples.EWordMapping.God;
                    break;
                case 1:
                    god = BookContentWithExamples.EWordMapping.Jesus;
                    break;
                case 2:
                    god = BookContentWithExamples.EWordMapping.Krishna;
                    break;
                case 3:
                    god = BookContentWithExamples.EWordMapping.Allah;
                    break;
                case 4:
                    god = BookContentWithExamples.EWordMapping.Buddha;
                    break;
                case 5:
                    god = BookContentWithExamples.EWordMapping.SpiritOfTheUniverse;
                    break;
                default:
                    god = BookContentWithExamples.EWordMapping.God;
                    PlayerPrefs.SetInt("god", godDropdown.value);
                    break;
            }
            switch (sexDropdown.value)
            {
                case 0:
                    sex = BookContentWithExamples.EWordMapping.Male;
                    break;
                case 1:
                    sex = BookContentWithExamples.EWordMapping.Female;
                    break;
                case 2:
                    sex = BookContentWithExamples.EWordMapping.NonBionary;
                    break;
                default:
                    sex = BookContentWithExamples.EWordMapping.Male;
                    PlayerPrefs.SetInt("sex", sexDropdown.value);
                    break;
            }
        }
        private void SaveSettings()
        {
            PlayerPrefs.SetInt("fellowship", fellowshipDropdown.value);
            PlayerPrefs.SetInt("god", godDropdown.value);
            PlayerPrefs.SetInt("sex", sexDropdown.value);

        }
        public void OnSettingsChange()
        {
            SaveSettings();
            ConvertDropdownValues();
            Parser.ClearWordMapping();
            ReadTextFile();
        }
        private void ReadTextFile()
        {
            //TextAsset bigBook = Resources.Load<TextAsset>("Book/BB.txt");
            StreamReader streamReader = new StreamReader(new MemoryStream(bigBook.bytes));
            string BBRaw = streamReader.ReadToEnd();
            streamReader.Close();
            
            chapters = bigBook.text.Split("{{Chapter}}");

            SetUpWordMapping(fellowship, god, sex);
        }

        private void SetUpWordMapping(BookContentWithExamples.EWordMapping EWMfellowship, BookContentWithExamples.EWordMapping EWMgod, BookContentWithExamples.EWordMapping EWMsex) 
        {
            Parser.AddWordMapping(BookContentWithExamples.EWordMapping.Alcohol);
            Parser.AddWordMapping(EWMfellowship);
            Parser.AddWordMapping(EWMgod);
            Parser.AddWordMapping(EWMsex);
            for (int i = 0; i<chapters.Length; ++i )
            {
                chapters[i] = Parser.ConvertAllReplaceableWords(chapters[i]);
            }
        
        
        }
        
        private void LoadJSON()
        {
            //StreamReader streamReader = new StreamReader(new MemoryStream(BBFull.bytes));
            //string jsonText = streamReader.ReadToEnd();
            //streamReader.Close();
            //string jsonText = BBFull.text;
            
            GetContent data = Newtonsoft.Json.JsonConvert.DeserializeObject<GetContent>(BBFull.text);
        }
    }
    [System.Serializable]
    public class GetContent
    {
        BookData[] content;
    }
    [System.Serializable]
    public class BookData
    {
        public string title;
        public int id;
        public Sections[] sections;
    }
    [System.Serializable]
    public class Sections
    {
        public string title;
        public string text;
    }
}
