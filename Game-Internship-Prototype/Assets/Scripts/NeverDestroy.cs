using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverDestroy : MonoBehaviour
{
    public GameObject[] NeverDestroyObj;
    private bool OnceBool;

    private void Awake() {
        if(!OnceBool){
            OnceBool = true;
            foreach(GameObject obj in NeverDestroyObj){
                DontDestroyOnLoad(obj);
            }
        }
    }
}
