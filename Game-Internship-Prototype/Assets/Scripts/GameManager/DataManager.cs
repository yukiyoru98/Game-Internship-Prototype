using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager self;
    public Data data;
    private void Awake() {
        self = this;
        // DefaultData();
    }

    void DefaultData(){
        data = new Data();
        data.Name = "";
        data.Money = 0;
        data.Chapter = 1;
        data.Progress = 0;
    }
}
