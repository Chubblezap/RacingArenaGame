﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineProjectile : BasicProjectile
{
    public GameObject explosionprefab;
    private bool set = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce((transform.forward*2) * speed);
    }

    public override void Detonate()
    {
        if(set)
        {
            GameObject exp = Instantiate(explosionprefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            exp.GetComponent<BombProjectileExplosion>().owner = owner;
            particles.transform.SetParent(null);
            particles.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            if (particles.GetComponent<ParticleSystem>() != null)
            {
                particles.GetComponent<ParticleSystem>().Stop();
            }
            particles.GetComponent<ProjectileParticle>().StartCoroutine("Destroy");
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(!set)
        {
            body.AddForce(transform.up * -0.5f);
        }
    }

    private void Set()
    {
        set = true;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<SphereCollider>().radius = 5;
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Environment" && !set)
        {
            Set();
        }
    }

}