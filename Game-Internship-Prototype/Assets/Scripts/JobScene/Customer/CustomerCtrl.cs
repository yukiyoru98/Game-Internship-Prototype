using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerCtrl : MonoBehaviour
{
    public static CustomerCtrl self;
    private void Awake()
    {
        self = this;
    }
    public SpriteRenderer CustomerSprite;
    public Vector2 XRange;
    public Vector2 YRange;
    public Sprite[] sprites;
    [SerializeField]
    private float MoveCD = 30f;
    private float MaxMoveCD = 0;
    [SerializeField]
    private bool CanMove = false;
    private bool isHit = false;
    private float WakeUpCD = 2f;
    private float MinDistance = 0.8f;
    [SerializeField]
    private int money;


    private void Start()
    {
        CustomerSprite = this.gameObject.GetComponent<SpriteRenderer>();
        //first move: move from out-of-screen into sight
        float x;
        if (transform.position.x < 0)
        { //if spawn at left
            x = Random.Range(XRange.x, 0);
        }
        else
        { //if spawn at right
            x = Random.Range(0, XRange.y);
        }
        float y = Random.Range(YRange.x, YRange.y);

        transform.DOMove(new Vector3(x, y, y), 1f).SetEase(Ease.Linear).OnComplete(ActivateMovement);

    }

    private void Update()
    {
        if (CanMove)
        {
            if (Time.time >= MaxMoveCD)
            {
                Move();
                MaxMoveCD = Time.time + MoveCD;
            }
        }
    }

    void ActivateMovement()
    {
        CanMove = true;
        MaxMoveCD = Time.time + MoveCD;
    }

    void Move()
    {
        float x, y;
        do
        {
            x = Random.Range(XRange.x, XRange.y);
            y = Random.Range(YRange.x, YRange.y);
        }
        while (Vector2.Distance(transform.position, new Vector2(x, y)) < MinDistance); //minimum move distance

        transform.DOMove(new Vector3(x, y, y), 1f).SetEase(Ease.Linear);
    }

    private void OnMouseDown()
    {
        if (!isHit) //first click
        {
            PlayerCtrl.self.Move(gameObject);
            CanMove = false;
            transform.DOPause();
        }
        else
        { //second click
            //player pull customer back
            //temp: just disappear
            CancelInvoke();
            Destroy(gameObject);
            DataManager.self.AddMoney(money);
            SpawnCustomer.self.AddCustomer(-1);
        }

    }

    public void Hit()
    {
        CustomerSprite.sprite = sprites[1];
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Fall", PlayerCtrl.self.HitSpriteCD);

    }

    public void Fall()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        isHit = true; //isHit = hit = true
        CustomerSprite.sprite = sprites[2];
        Invoke("WakeUp", WakeUpCD);
    }

    // public IEnumerator Fall()
    // {
    //     CustomerSprite.sprite = sprites[1];
    //     GetComponent<BoxCollider2D>().enabled = false;
    //     yield return new WaitForSeconds(1f);
    //     GetComponent<BoxCollider2D>().enabled = true;
    //     isHit = true; //isHit = hit = true
    //     CustomerSprite.sprite = sprites[2];

    //     // yield return new WaitForSeconds(WakeUpCD);
    //     // isHit = false;
    //     // CanMove = true;
    //     // CustomerSprite.sprite = sprites[0];
    //     Invoke("WakeUp", WakeUpCD);

    // }

    void WakeUp()
    {
        isHit = false;
        CanMove = true;
        CustomerSprite.sprite = sprites[0];
    }

}
