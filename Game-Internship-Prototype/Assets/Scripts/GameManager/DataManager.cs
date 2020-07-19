using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager self;
    public Data data;
    public delegate void OnSetMoney();
    public static event OnSetMoney OnSetMoneyEvent;
    public delegate void OnSetProgress();
    public static event OnSetProgress OnSetProgressEvent;
    public delegate void OnSetChapter();
    public static event OnSetChapter OnSetChapterEvent;
    private void Awake() {
        self = this;
        // DefaultData();
    }

    public void DefaultData(){
        data = new Data();
        data.Name = "";
        data.Money = 0;
        data.Chapter = 1;
        data.Progress = 0;
    }

    public void SetName(string str){
        data.Name = str;
    }

    public void AddMoney(int m){
        SetMoney(data.Money + m);
    }

    public void AddProgress(int p){
        SetProgress(data.Progress + p);
    }

    public void AddChapter(int ch){
        SetChapter(data.Chapter + ch);
    }

    public void SetMoney(int m){
        data.Money = m;
        if(OnSetMoneyEvent != null){
            Debug.Log("SetMoneyEvent");
            OnSetMoneyEvent();
        }
    }

    public void SetProgress(int p){
        // Debug.Log("set progress " + p);
        data.Progress = p;
        if(data.Progress > 100){
            AddChapter(1);
        }
        if(OnSetProgressEvent != null){
            Debug.Log("SetProgressEvent");
            OnSetProgressEvent();
        }
    }

    public void SetChapter(int ch){
        // Debug.Log("set chapter " + ch);
        data.Chapter = ch;
        data.Progress = 0;
        if(OnSetChapterEvent != null){
            Debug.Log("SetChapterEvent");
            OnSetChapterEvent();
        }
    }
}
