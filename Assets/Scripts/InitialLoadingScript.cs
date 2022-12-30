using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BookMechanics
{
    public class InitialLoadingScript : MonoBehaviour
    {
        public string[] chapters;
        private void OnEnable()
        {
            ReadTextFile();
        }
        private void ReadTextFile()
        {
            StreamReader streamReader = new StreamReader("Assets/Book/BB.txt");
            string BBRaw = streamReader.ReadToEnd();
            streamReader.Close();

            chapters = BBRaw.Split("{{Chapter}}");
        }
    }
}
