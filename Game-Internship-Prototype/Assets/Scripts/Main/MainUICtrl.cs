using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUICtrl : MonoBehaviour
{
    public Button StoryBtn, JobBtn; //GuideBtn, EventBtn;

    public void ClickStoryBtn(){
        LoadingScenes.self.ChangeScene(Tags.STORY_SCENE);
    }

    public void ClickJobBtn(){
        LoadingScenes.self.ChangeScene(Tags.JOB_SCENE);
    }
}
