using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BasicProjectile
{
    public GameObject explosionprefab;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce((transform.up*4 + transform.forward*3) * speed);
    }

    public override void Detonate()
    {
        Instantiate(explosionprefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        StartCoroutine("DelayDetonate");
    }

    private void FixedUpdate()
    {
        body.AddForce(transform.up * -0.5f);
    }
}
