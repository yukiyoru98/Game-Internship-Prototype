using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessagePanelCtrl : MonoBehaviour
{
    public Text ErrorText;
    public static ErrorMessagePanelCtrl self;
    private void Awake() {
        self = this;
    }
    public void SetMessage(string mes){
        ErrorText.text = mes;
    }

}
