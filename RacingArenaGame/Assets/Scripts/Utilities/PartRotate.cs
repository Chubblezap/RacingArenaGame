using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartRotate : MonoBehaviour
{
    public Transform center;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(center.position, center.up, 1);
    }
}
