using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaBeam : BasicProjectile
{
    private float length;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        length = transform.localScale.y;
        width = transform.localScale.x;
        transform.localPosition = new Vector3(0, 0.5f, 1003);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(lifetime >= 3.5f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(1, length, 1), new Vector3(width, length, width), 1 - (lifetime - 3.5f)*2);
        }
        else if(lifetime <= 1f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(0, length, 0), new Vector3(width, length, width), lifetime);
        }

        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Detonate();
        }
    }

    override protected void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Vehicle")
        {
            //
        }
    }
}
