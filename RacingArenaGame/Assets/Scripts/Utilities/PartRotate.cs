using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartRotate : MonoBehaviour
{
    public Transform center;
    public float speed = 1;
    public string axis = "Up";

    // Update is called once per frame
    void FixedUpdate()
    {
        if(axis == "Up")
        {
            transform.RotateAround(center.position, center.up, speed);
        }
        else if(axis == "Angle45")
        {
            transform.RotateAround(center.position, center.up + center.forward, speed);
        }
    }
}
