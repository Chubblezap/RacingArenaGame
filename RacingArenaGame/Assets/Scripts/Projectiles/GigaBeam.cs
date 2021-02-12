using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaBeam : BasicProjectile
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = owner;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifetime >= 3.5f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(1, 50, 1), new Vector3(5, 50, 5), 1 - (lifetime - 3.5f)*2);
        }
        else if(lifetime <= 1f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(5, 50, 5), new Vector3(0, 50, 0), lifetime);
        }

        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Detonate();
        }
    }
}
