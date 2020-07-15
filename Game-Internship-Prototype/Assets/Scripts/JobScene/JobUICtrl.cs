using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobUICtrl : MonoBehaviour
{
    public Button HomeBtn; //, CouponBtn, AutoBtn;

    public void ClickHomeBtn(){
        LoadingScenes.self.ChangeScene(Tags.MAIN_SCENE);
    }

}
