using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BasicProjectile
{
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce((transform.up*4 + transform.forward*3) * speed);
    }

    private void FixedUpdate()
    {
        body.AddForce(transform.up * -0.5f);
    }
}
