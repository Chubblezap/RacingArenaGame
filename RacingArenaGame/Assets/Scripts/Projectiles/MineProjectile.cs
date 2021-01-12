using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineProjectile : BasicProjectile
{
    public GameObject explosionprefab;
    public GameObject DetectField;
    private bool set = false;
    private bool armed = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce((transform.forward * speed * 4) + Vector3.up*4);
        body.AddTorque(0, Random.Range(-2f, 2f), 0);
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
            body.velocity = new Vector3(body.velocity.x * 0.96f, body.velocity.y, body.velocity.z * 0.96f);
        }
    }

    private void Set()
    {
        set = true;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<SphereCollider>().isTrigger = true;
        DetectField.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine("RadialGrow");
    }

    private IEnumerator RadialGrow()
    {
        float timer = 0;
        while (timer < 1)
        {
            GetComponent<SphereCollider>().radius = 5 * Mathf.Sin(timer * Mathf.PI * 0.5f);
            DetectField.transform.localScale = new Vector3(GetComponent<SphereCollider>().radius * 2, GetComponent<SphereCollider>().radius * 2, GetComponent<SphereCollider>().radius * 2);
            timer += Time.deltaTime;
            yield return null;
        }
        armed = true;
        DetectField.GetComponent<MeshRenderer>().material.SetColor("_BaseTint", new Color(0, 1, 0));
        DetectField.GetComponent<MeshRenderer>().material.SetColor("_GlowTint", new Color(0, 1, 0));
        yield return null;
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Vehicle" && armed)
        {
            Detonate();
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Environment" && !set)
        {
            Set();
            transform.up -= (transform.up - collision.GetContact(0).normal);
        }
    }

}
