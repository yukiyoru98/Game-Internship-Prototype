using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour //StoryScene/DialogueUI
{
    public TextAsset storyFile;
    public Text NameText;
    public Text DialogueText;
    public GameObject BackgroundObj, ClickObj;
    [SerializeField]
    private string[] storyLines;
    public Dictionary<string, PortraitInfo> PortraitInfoDict = new Dictionary<string, PortraitInfo>();
    public List<GameObject> Portraits = new List<GameObject>();
    private bool timer = false;
    private int sentenceID;
    private int charID;
    [SerializeField]
    private float printSpeed;

    private void Start()
    {
        if (ReadStoryFile())
        {
            ClickObj.SetActive(true);
            Next();
        }
    }

    bool ReadStoryFile()
    {
        storyFile = Resources.Load<TextAsset>(Tags.STORY_FILE_PREFIX + DataManager.self.data.Chapter.ToString() + "-" + DataManager.self.data.Progress.ToString());
        if (!storyFile)
        {
            Debug.LogError("Failed to load story");
            return false;
        }
        // Debug.Log("Load Story " + storyFile.name);

        storyLines = storyFile.text.Split('\n');
        int n = int.Parse(storyLines[0]); //how many characters in this dialogue
        for (int i = 0; i < n; i++)
        { //get the information of characters
            string[] tmp = (storyLines[i + 1].Remove(storyLines[i + 1].Length - 1)).Split(','); //potrait folder name:display name
            PortraitInfo info = new PortraitInfo();
            info.positionID = -1; //initially not on display

            if (tmp[1] == "***")
            {
                info.Name = DataManager.self.data.Name;
            }
            else
            {
                info.Name = tmp[1];
            }

            PortraitInfoDict.Add(tmp[0], info);
        }
        // foreach (var x in PortraitInfoDict){
        //     Debug.Log(x.Key + " " +　x.Value.Name);
        // }
        sentenceID = n + 1;
        return true;
    }

    public void Next()
    {
        if (sentenceID >= storyLines.Length)
        { //end dialogue
            Debug.Log("End Dialogue");
            ClickObj.SetActive(false);
            DataManager.self.AddProgress(1);
            LoadingScenes.self.ChangeScene(Tags.MAIN_SCENE);
        }
        else if (storyLines[sentenceID][0] == '#')
        { //set background
            BackgroundObj.GetComponent<BackgroundCtrl>().LoadBG(storyLines[sentenceID].Substring(1, storyLines[sentenceID].Length - 2));
            sentenceID++;
            Next();
        }
        else if (storyLines[sentenceID][0] == '$')
        { //set character
            ChangeCharacter();
            sentenceID++;
            Next();
        }
        else if (storyLines[sentenceID][0] == '@')
        { //set character portrait

            // Debug.Log(storyLines[sentenceID]);
            ChangePortrait();
            sentenceID++;
            Next();
        }
        else if (storyLines[sentenceID][0] == '-')
        { //clear all text
            NameText.text = "";
            DialogueText.text = "";
            sentenceID++;
        }
        else
        { //print text
          //   Debug.Log(storyLines[sentenceID]);
            PrintText();
        }

    }

    void PrintText()
    {
        string[] tmp = storyLines[sentenceID].Split(':'); //tag : name : sentence
        // Debug.Log(tmp[0]+" "+tmp[1]+" "+tmp[2]);

        //Brighten Speaker portrait and name
        SetupSpeaker(tmp[0], tmp[1]);


        if (timer)
        { //already typing
            // Debug.Log("ShowFull");
            timer = false;
            DialogueText.text = tmp[2]; //directly show full sentence
            sentenceID++;
        }
        else
        {
            // Debug.Log("Typer");
            timer = true;
            StartCoroutine(TypewriterPrint(tmp[2])); //typewriter effect
        }
    }

    IEnumerator TypewriterPrint(string sentence)
    {
        //initialize dialogue text
        DialogueText.text = "";
        charID = 0;
        while (timer)
        {
            DialogueText.text += sentence[charID];
            charID++;
            if (charID >= sentence.Length)
            { //sentence end
                timer = false;
                sentenceID++;
                // Debug.Log("End "+ sentenceID);
                break;
            }
            yield return new WaitForSeconds(printSpeed);
        }
    }

    void ChangePortrait()
    {
        storyLines[sentenceID] = storyLines[sentenceID].Substring(1, storyLines[sentenceID].Length - 2); //get rid of @ and 'n
        string[] tmp = storyLines[sentenceID].Split('>'); //tag, portraitID
        string character_tag = tmp[0];
        int newPortraitID = int.Parse(tmp[1]);
        Portraits[PortraitInfoDict[character_tag].positionID].GetComponent<PotraitCtrl>().ChangePortrait(newPortraitID);
        // print("Change Portrait" + character_tag + "-> " + Portraits[PortraitInfoDict[character_tag].positionID].GetComponent<PotraitCtrl>().portraits[newPortraitID].name);
    }

    void ChangeCharacter()
    { // change the character that is currently on display
        storyLines[sentenceID] = storyLines[sentenceID].Substring(1, storyLines[sentenceID].Length - 2); //get rid of $ and 'n
        string[] tmp = storyLines[sentenceID].Split('>'); //positionID, character tag
        int pos = int.Parse(tmp[0]);
        string character_tag = tmp[1];
        if (character_tag != "-")
        {
            PortraitInfoDict[character_tag].positionID = pos;
            Portraits[pos].GetComponent<PotraitCtrl>().LoadPortraits(character_tag);
        }
        else
        { //hide portrait
            Portraits[pos].SetActive(false);
        }
    }

    void SetupSpeaker(string speaker_tag, string Name)
    { //brighten the speaker's portrait and darken the others

        int ID;
        ID = (speaker_tag == "") ? (-1) : (PortraitInfoDict[speaker_tag].positionID); //if no speaker portrait->all dark

        for (int i = 0; i < Portraits.Count; i++)
        {
            bool speak = (i == ID);
            Portraits[i].GetComponent<PotraitCtrl>().Speak(speak);
        }


        //setup name displayed
        if (speaker_tag != "" && Name == "")
        { //if there is a speaker and name is not given
            Name = PortraitInfoDict[speaker_tag].Name; //use the name recorded in dict
        }
        
        NameText.text = Name == "" ? Name : "【" + Name + "】";

    }

}
