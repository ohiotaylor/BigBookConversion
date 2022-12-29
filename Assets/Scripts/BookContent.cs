using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class BookContent : MonoBehaviour
{
    public GameObject readingBox;

    public TextMeshProUGUI readingBoxText;
    string[] chapters;
    int location = 0;
    int conversion = 0;
    public bool selected = false;
    string forward = "#1 WE, OF [Alcoholics Anonymous], are more than one hundred men and women who have recovered from a seemingly hopeless state of mind and body.To show other [alcoholics] precisely how we have recovered is the main purpose of this book. For them, we hope these pages will prove so convincing that no further authentication will be necessary. We think this account of our experiences will help everyone to better understand the [alcoholic]. Many do not comprehend that the [alcoholic] is a very sick person. And besides, we are sure that our way of living has its advantages for all. It is important that we remain anonymous because we are too few, at present to handle the overwhelming number of personal appeals which may result from this publication. Being mostly business or professional folk, we could not well carry on our occupations in such an event. We would like it understood that our [alcoholic] work is an avocation. When writing or speaking publicly about [alcoholism], we urge each of our Fellowship to omit his personal name, designating himself instead as a member of [Alcoholics Anonymous]. Very earnestly we ask the press also, to observe this request, for otherwise we shall be greatly handicapped. We are not an organization in the conventional sense of the word. There are no fees or dues whatsoever. The only requirement for membership is an honest desire to stop [drinking]. We are not allied with any particular faith, sect or denomination, nor do we oppose anyone. We simply wish to be helpful to those who are afflicted. We shall be interested to hear from those who are getting results from this book, particularly from those who have commenced work with other [alcoholics]. We should like to be helpful to such cases. Inquiry by scientific, medical, and religious societies will be welcomed. [Alcoholics Anonymous].";

    string drOp = "WE OF [Alcoholics Anonymous] believe that the reader will be interested in the medical estimate of the plan of recovery described in this book. Convincing testimony must surely come from medical men who have had experience with the sufferings of our members and have witnessed our return to health. A well-known doctor, chief physician at a nationally prominent hospital specializing in alcoholic and drug addiction, gave Alcoholics Anonymous this letter: \nTo Whom It May Concern:\nI have specialized in the treatment of [alcoholism] for many years.\nIn late 1934 I attended a patient who, though he had been a competent businessman of good earning capacity, was an [alcoholic] of a type I had come to regard as hopeless.\nIn the course of his third treatment he acquired certain ideas concerning a possible means of recovery. As part of his rehabilitation he commenced to present his conceptions to other [alcoholics], impressing upon them that they must do likewise with still others. This has become the basis of a rapidly growing fellowship of these men and their families. This man and over one hundred others appear to have recovered.\nI personally know scores of cases who were of the type with whom other methods had failed completely.\nThese facts appear to be of extreme medical importance; because of the extraordinary possibilities of rapid growth inherent in this group they may mark a new epoch in the annals of [alcoholism]. These men may well have a remedy for thousands of such situations.\nYou may rely absolutely on anything they say about themselves.\nVery truly yours,\nWilliam D. Silkworth, M.D.\nThe physician who, at our request, gave us this letter, has been kind enough to enlarge upon his views in another statement which follows. In this statement he confirms what we who have suffered [alcoholic] torture must believe—that the body of the [alcoholic] is quite as abnormal as his mind. It did not satisfy us to be told that we could not control our [drinking] just because we were maladjusted to life, that we were in full flight from reality, or were outright mental defectives. These things were true to some extent, in fact, to a considerable extent with some of us. But we are sure that our bodies were sickened as well. In our belief, any picture of the [alcoholic] which leaves out this physical factor is incomplete.\nThe doctor’s theory that we have an allergy to [alcohol] interests us. As laymen, our opinion as to its soundness may, of course, mean little. But as ex-problem [drinkers], we can say that his explanation makes good sense. It explains many things for which we cannot otherwise account.\nThough we work out our solution on the spiritual as well as an altruistic plane, we favor hospitalization for the [alcoholic] who is very jittery or befogged. More often than not, it is imperative that a man’s brain be cleared before he is approached, as he has then a better chance of understanding and accepting what we have to offer.  ";

    private void OnEnable()
    {

        ReadTextFile();
        switch (location)
        {
            case 0: UpdateText(chapters[8]); break;
        }
        //SetConversion();
    }
    void Start()
    {
        

    }
    private void ReadTextFile()
    {
        StreamReader streamReader = new StreamReader("Assets/Book/BB.txt");
        string BBRaw = streamReader.ReadToEnd();
        streamReader.Close();

        chapters = BBRaw.Split("{{Chapter}}");
       

    }

    void Update()
    {

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
    public void SwitchBool()
    {
        selected = selected ? false : true;
        SetConversion();
    }
    public void SetConversion()
    {
        if (selected)
            conversion = 0;
        else
            conversion = 1;
    }
    public void ChangeChapter(int chapter)
    {
        conversion = chapter;
        string text = "";
        int bottonLength = -1000;
        switch (chapter)
        {
            case 0:
                text = forward;
                bottonLength = -1022;
                break;
            case 1:
                text = drOp;
                bottonLength = -1800;
                break;
        }
        readingBoxText.rectTransform.SetBottom(bottonLength);
        UpdateText(text);
    }
}
     