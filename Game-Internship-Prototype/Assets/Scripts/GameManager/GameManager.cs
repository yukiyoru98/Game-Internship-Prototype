using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DataManager dataManager;

    public static GameManager self;
    private void Awake() {
        self = this;
    }

}
