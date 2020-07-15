using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScenes : MonoBehaviour
{
    private Animation anim;

    public static LoadingScenes self;
    private void Awake() {
        self = this;
        anim = GetComponent<Animation>();
    }

    public void ChangeScene(string scene){
        StartCoroutine(Go(scene));
    }

    IEnumerator Go(string scene){
        anim.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }
}
