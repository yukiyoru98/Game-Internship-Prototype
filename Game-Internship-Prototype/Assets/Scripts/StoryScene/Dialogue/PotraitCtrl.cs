using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotraitCtrl : MonoBehaviour //StoryScene/Portrait_L(R)
{
    public Image currentPortrait;
    public Sprite[] portraits;
    private Color32 Bright = new Color32(255, 255, 255, 255);
    private Color32 Dark = new Color32(155, 155, 155, 255);

    private void Awake() {
        currentPortrait = GetComponent<Image>();
    }

    public void LoadPortraits(string charName){
        // Debug.Log(Tags.CHARACTERS_PATH + charName + "/Portraits");
        portraits = Resources.LoadAll<Sprite>(Tags.CHARACTERS_PATH + charName + "/Portraits");
        
    }

    public void ChangePortrait(int ID){
        // Debug.Log("Change");
        this.gameObject.SetActive(true);
        currentPortrait.sprite = portraits[ID];
    }

    public void Speak(bool speak){
        // Debug.Log(name + "speak:"+speak);
        currentPortrait.color = speak? Bright : Dark;
    }
}
