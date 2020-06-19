using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartRotate : MonoBehaviour
{
    public Transform center;
    public float speed = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(center.position, center.up, speed);
    }
}
