using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldGun : MonoBehaviour
{
    public Transform owner;
    public string gunType;
    
    void FixedUpdate()
    {
        if(owner != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, owner.position + new Vector3(0, 1, 0), 100 * Time.deltaTime);
        }
    }
}
