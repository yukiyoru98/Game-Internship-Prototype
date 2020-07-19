using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public Vector2 XPos;
    public Vector2 YRange;
    public float SpawnCD;
    public float MaxSpawnCD;
    public bool CanSpawn;
    private GameObject customer;
    public int numberToSpawn = 4;
    private int CurrentCount = 0;
    public int MaxCount = 10;

    private void Start()
    {
        customer = Resources.Load(Tags.CUSTOMER_PREFAB) as GameObject;
    }

    private void Update()
    {
        if (CanSpawn && Time.time >= MaxSpawnCD)
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                GameObject obj = Instantiate(customer, transform);
                obj.name = CurrentCount.ToString();
                //set initial position
                float x = Random.Range(0, 2) == 0 ? XPos.x : XPos.y; //determines whether to spawn at left or right
                float y = Random.Range(YRange.x, YRange.y); //random select Y position
                obj.transform.position = new Vector3(x, y, y);

                CurrentCount++;

                if (CurrentCount == MaxCount)
                {
                    CanSpawn = false;
                    break;
                }
            }
            MaxSpawnCD = Time.time + SpawnCD;
        }

        if (CurrentCount < MaxCount)
        {
            CanSpawn = true;
        }
    }
}
