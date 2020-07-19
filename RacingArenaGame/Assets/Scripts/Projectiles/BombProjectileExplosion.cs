using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectileExplosion : MonoBehaviour
{
    private GameObject[] hits = new GameObject[3];
    private int numhits = 0;
    public float damage;
    public float force;
    public Transform owner;

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

        if(!alreadyhit && numhits < 3)
        {
            hits[numhits] = collidedobject;
            numhits++;
        }
    }
}
