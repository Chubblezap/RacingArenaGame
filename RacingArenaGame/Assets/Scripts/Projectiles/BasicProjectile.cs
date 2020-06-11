using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public Transform owner;
    public float damage;
    public float speed;
    public float force;
    public float lifetime;
    protected Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime < 0)
        {
            Detonate();
        }
    }

    public void Detonate()
    {
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Environment")
        {
            // do particles
            Detonate();
        }
    }
}
