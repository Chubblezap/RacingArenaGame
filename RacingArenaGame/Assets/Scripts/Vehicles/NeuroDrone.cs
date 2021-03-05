using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuroDrone : MonoBehaviour
{
    public GameObject target;
    public Transform owner;
    private bool returning = false;

    // Update is called once per frame
    void Update()
    {
        if(!returning && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.1f);
            if(Vector3.Distance(transform.position, target.transform.position) < 0.5f)
            {
                target.transform.SetParent(transform);
                returning = true;
            }
        }
        else if(returning && owner != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, owner.position, 0.1f);
            if (target == null || Vector3.Distance(transform.position, owner.transform.position) < 0.2f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
