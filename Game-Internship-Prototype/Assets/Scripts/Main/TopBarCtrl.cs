using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarCtrl : MonoBehaviour
{
    public static TopBarCtrl self;
    public Slider ProgressBar;
    public Text MoneyText, ProgressText, ChapterText, TitleText;
    [SerializeField]
    private List<string[]> Titles = new List<string[]>();
    public TextAsset TitlesFile;
    private const string ProgressTextSuffix = "/100";

    private void Awake()
    {
        self = this;
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

    private void OnEnable() {
        DataManager.OnSetMoneyEvent += SetMoney;
        // DataManager.OnSetProgressEvent += SetProgress;
        // DataManager.OnSetChapterEvent += SetChapter;
    }

    private void OnDisable() {
        DataManager.OnSetMoneyEvent -= SetMoney;
        // DataManager.OnSetProgressEvent -= SetProgress;
        // DataManager.OnSetChapterEvent -= SetChapter;
    }
    
    private void Start()
    {
        SetChapter();
        SetProgress();
        SetMoney();
    }

    public void SetMoney()
    {
        // Debug.Log("Money UI updated");
        MoneyText.text = DataManager.self.data.Money.ToString();
    }
    public void SetProgress()
    {
        Debug.Log("Progress UI updated");
        int value = DataManager.self.data.Progress * 25;
        ProgressText.text = value.ToString() + ProgressTextSuffix;
        ProgressBar.value = value;
    }

    public void SetChapter()
    {
        Debug.Log("Chapter UI updated");
        ChapterText.text = Titles[DataManager.self.data.Chapter][0];
        TitleText.text = Titles[DataManager.self.data.Chapter][1];
    }
}
