using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectileExplosion : MonoBehaviour
{
    public float lifetime = 3;

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
