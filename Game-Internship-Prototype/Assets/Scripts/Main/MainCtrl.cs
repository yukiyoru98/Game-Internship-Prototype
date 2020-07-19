using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCtrl : MonoBehaviour
{
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
        if(DataManager.self.data.Money < storyCost[DataManager.self.data.Chapter]){
            Debug.Log("不夠錢！");
            return;
        }
        LoadingScenes.self.ChangeScene(Tags.STORY_SCENE);
        DataManager.self.AddMoney(-storyCost[DataManager.self.data.Chapter]);
    }

    public void ClickJobBtn()
    {
        LoadingScenes.self.ChangeScene(Tags.JOB_SCENE);
    }

}
