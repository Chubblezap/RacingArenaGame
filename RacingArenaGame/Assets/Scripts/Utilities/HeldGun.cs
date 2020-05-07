using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldGun : MonoBehaviour
{
    public Transform owner;
    public string gunType;
    private float curSpeed = 15;

    void Update()
    {
        if(curSpeed <= 100)
        {
            curSpeed += Time.deltaTime * 2;
        }
    }

    void FixedUpdate()
    {
        if(owner != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, owner.position + new Vector3(0, 1, 0), curSpeed * Time.deltaTime);
        }
    }
}
