using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverDestroy : MonoBehaviour
{
    public GameObject[] NeverDestroyObj;
    private static bool OnceBool;

    private void Awake() {
        if(!OnceBool){
            OnceBool = true;
            Debug.Log("Instantiate NeverDestroy Objects");
            foreach(GameObject obj in NeverDestroyObj){
                DontDestroyOnLoad(Instantiate(obj) as GameObject);
            }
        }
    }
}
