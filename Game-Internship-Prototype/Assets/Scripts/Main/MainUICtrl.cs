using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUICtrl : MonoBehaviour
{
    public Button StoryBtn, JobBtn; //GuideBtn, EventBtn;
    public Slider ProgressBar;
    public Text MoneyText, ProgressText, ChapterText, TitleText;
    [SerializeField]
    private List<string[]> Titles = new List<string[]>();
    public TextAsset TitlesFile;
    private const string ProgressTextSuffix = "/100";

    private void Awake()
    {
        if (TitlesFile)
        {
            string[] tmp = TitlesFile.text.Split('\n');
            for (int i = 0; i < tmp.Length; i++)
            {
                string[] sub_tmp = tmp[i].Split('@');
                // Debug.Log(sub_tmp[0] + " " + sub_tmp[1]);
                Titles.Add(sub_tmp);
            }
        }
    }

    private void Start()
    {
        SetChapter();
        SetProgress();
        SetMoney();
    }

    public void ClickStoryBtn()
    {
        LoadingScenes.self.ChangeScene(Tags.STORY_SCENE);
    }

    public void ClickJobBtn()
    {
        LoadingScenes.self.ChangeScene(Tags.JOB_SCENE);
    }

    public void SetMoney()
    {
        MoneyText.text = DataManager.self.data.Money.ToString();
    }

    public void SetProgress()
    {
        int value = DataManager.self.data.Progress;
        ProgressText.text = value.ToString() + ProgressTextSuffix;
        ProgressBar.value = value;
    }

    public void SetChapter()
    {
        ChapterText.text = Titles[DataManager.self.data.Chapter][0];
        TitleText.text = Titles[DataManager.self.data.Chapter][1];
    }

}
