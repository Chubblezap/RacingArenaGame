using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViralProjectile : BasicProjectile
{
    public float spinspeed;
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.forward, spinspeed);
    }
}
