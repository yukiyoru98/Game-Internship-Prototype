using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCtrl : MonoBehaviour //StoryScene/Background
{
    private Image BG_Image;

    private void Awake() {
        BG_Image = GetComponent<Image>();
    }

    public void LoadBG(string path){
        BG_Image.sprite = Resources.Load<Sprite>(Tags.BG_PATH + path);
    }
}
