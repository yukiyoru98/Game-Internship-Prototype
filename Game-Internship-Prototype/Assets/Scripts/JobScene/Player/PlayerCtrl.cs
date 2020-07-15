using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl self;
    private void Awake() {
        self = this;
    }
    
    public Sprite[] sprites;
    private Vector3 OriginalPosition;

    private void Start() {
        OriginalPosition = transform.position;
    }

    public void Move(Vector3 target){ //move towards customer
        // Debug.Log("Move to target");
        transform.DOMove(target, 0.5f).SetEase(Ease.Linear).OnComplete(Attack);
    }

    public void Attack(){
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        StartCoroutine(CustomerCtrl.self.Fall());
        Invoke("IdleSprite", 0.5f);
    }

    void IdleSprite(){
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }


}
