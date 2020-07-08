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

    private void FixedUpdate()
    {
        body.AddForce(transform.up * -0.5f);
    }
}
