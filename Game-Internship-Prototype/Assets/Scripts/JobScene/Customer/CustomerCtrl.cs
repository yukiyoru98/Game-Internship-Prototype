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
    public Vector2 XRange;
    public Vector2 YRange;
    public Sprite[] sprites;
    [SerializeField]
    private float MoveCD = 30f;
    private float MaxMoveCD = 0;
    private bool CanMove = true;
    private bool isHit = false;
    private float WakeUpCD = 2f;
    private float MaxWakeUpCD;

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
        float x = Random.Range(XRange.x, XRange.y);
        float y = Random.Range(YRange.x, YRange.y);
        transform.DOMove(new Vector3(x, y, transform.position.z), 1f).SetEase(Ease.Linear);
    }

    private void OnMouseDown()
    {
        if (!isHit) //first click
        {
            PlayerCtrl.self.Move(transform.position);
            CanMove = false;
        }
        else
        { //second click
            //player pull customer back
            //temp: just disappear
            Destroy(gameObject);
        }

    }

    public IEnumerator Fall()
    {
        Debug.Log("Fall");
        isHit = true; //isHit = hit = true
        GetComponent<SpriteRenderer>().sprite = sprites[1];

        yield return new WaitForSeconds(WakeUpCD);
        isHit = false;
        CanMove = true;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
