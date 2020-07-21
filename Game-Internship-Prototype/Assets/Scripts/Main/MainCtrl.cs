using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCtrl : MonoBehaviour
{
    public GameObject ErrorMessagePanel;
    public Button StoryBtn, JobBtn; //GuideBtn, EventBtn;
    public TextAsset StoryCostFile;
    [SerializeField]
    List<int> storyCost = new List<int>();

    private void Awake() {
        if(StoryCostFile){
            foreach(string str in StoryCostFile.text.Split('\n')){
                storyCost.Add(int.Parse(str.Split(':')[1])); //each line contains chapter:cost
            }
        }
    }
    
    public void ClickStoryBtn()
    {

        if(DataManager.self.data.EndGame){
            ErrorMessage(true);
            ErrorMessagePanelCtrl.self.SetMessage("已完成所有劇情。");
            return;
        }
        
        int next = DataManager.self.NextChapter();
        // Debug.Log(next);

        if(DataManager.self.data.Money < storyCost[next]){
            Debug.Log("不夠錢！");
            ErrorMessage(true);
            ErrorMessagePanelCtrl.self.SetMessage("金錢不足，無法觀看劇情。");
            return;
        }
        LoadingScenes.self.ChangeScene(Tags.STORY_SCENE);
        DataManager.self.AddMoney(-storyCost[next]);
    }

    public void ClickJobBtn()
    {
        LoadingScenes.self.ChangeScene(Tags.JOB_SCENE);
    }

    public void ErrorMessage(bool err){
        ErrorMessagePanel.SetActive(err);
        StoryBtn.interactable = !err;
        JobBtn.interactable = !err;
    }
}
