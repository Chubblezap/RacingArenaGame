using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public Transform endpoint;

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collidedobject = collision.gameObject;
        if(collidedobject.tag == "Vehicle" && collidedobject.GetComponent<BaseVehicle>().player != 0)
        {
            collidedobject.GetComponent<BaseVehicle>().doMoveAlongCurve(collidedobject.transform.position, endpoint.position);
        }
    }
}
