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
    private SpriteRenderer PlayerSprite;
    public Sprite[] sprites;
    private Vector3 OriginalPosition;
    private float Speed = 0.2f;
    public float HitSpriteCD = 0.3f;
    public GameObject TargetCustomer;

    private void Start() {
        PlayerSprite = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        OriginalPosition = transform.position;
    }

    public void Move(GameObject target){ //move towards customer
        // Debug.Log("Move to target: " + target);
        TargetCustomer = target;
        transform.DOMove(TargetCustomer.transform.position, Speed).SetEase(Ease.Linear).OnComplete(Attack);
    }

    public void Attack(){
        PlayerSprite.sprite = sprites[1];
        Invoke("AttackEnd", HitSpriteCD);
        // StartCoroutine(CustomerCtrl.self.Fall());
        // StartCoroutine(TargetCustomer.GetComponent<CustomerCtrl>().Fall());
        TargetCustomer.SendMessage("Hit");
    }

    void AttackEnd(){
        PlayerSprite.sprite = sprites[0];
    }


}
