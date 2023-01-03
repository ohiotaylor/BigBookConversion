using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BookMechanics
{
    public class InitialLoadingScript : MonoBehaviour
    {
        public string[] chapters;
        [SerializeField] private BookContentWithExamples Parser;
        BookContentWithExamples.EWordMapping fellowship;
        BookContentWithExamples.EWordMapping god;
        BookContentWithExamples.EWordMapping sex;
        private void OnEnable()
        {
            GrabUserSettings();
            ReadTextFile();
        }

        private void GrabUserSettings()
        {
            fellowship = BookContentWithExamples.EWordMapping.Sex;
            god = BookContentWithExamples.EWordMapping.God;
            sex = BookContentWithExamples.EWordMapping.Male;
        }
        private void ReadTextFile()
        {
            StreamReader streamReader = new StreamReader("Assets/Book/BB.txt");
            string BBRaw = streamReader.ReadToEnd();
            streamReader.Close();

            chapters = BBRaw.Split("{{Chapter}}");

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
    }
}
