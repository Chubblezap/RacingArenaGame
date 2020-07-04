using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectileExplosion : MonoBehaviour
{
    public float lifetime = 3;
    private GameObject[] hits;

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        bool alreadyhit = false;
        GameObject collidedobject = collision.gameObject;
        for(int i=0; i<hits.Length; i++)
        {
            if(hits[i] == collidedobject)
            {
                alreadyhit = true;
            }
        }

        if(!alreadyhit)
        {
            
        }
    }
}
