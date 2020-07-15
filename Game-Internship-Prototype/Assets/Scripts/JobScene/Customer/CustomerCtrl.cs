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
    private SpriteRenderer CustomerSprite;
    public Vector2 XRange;
    public Vector2 YRange;
    public Sprite[] sprites;
    [SerializeField]
    private float MoveCD = 30f;
    private float MaxMoveCD = 0;
    private bool CanMove = true;
    private bool isHit = false;
    private float WakeUpCD = 2f;
    

    private void Start(){
        CustomerSprite = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
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

    void Move()
    {
        float x, y; 
        do
        {
            x = Random.Range(XRange.x, XRange.y);
            y = Random.Range(YRange.x, YRange.y);
        }
        while (Vector2.Distance(transform.position, new Vector2(x, y)) < 0.8f);

        transform.DOMove(new Vector3(x, y, y), 1f).SetEase(Ease.Linear);
    }

    private void OnMouseDown()
    {
        if (!isHit) //first click
        {
            PlayerCtrl.self.Move(transform.position);
            CanMove = false;
            transform.DOPause();
        }
        else
        { //second click
            //player pull customer back
            //temp: just disappear
            Destroy(gameObject);
            //TODO:add money
        }

    }

    public IEnumerator Fall()
    {
        CustomerSprite.sprite = sprites[1];
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Fall");
        isHit = true; //isHit = hit = true
        CustomerSprite.sprite = sprites[2];

        yield return new WaitForSeconds(WakeUpCD);
        isHit = false;
        CanMove = true;
        CustomerSprite.sprite = sprites[0];
    }
}
