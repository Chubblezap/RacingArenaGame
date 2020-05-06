using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public string itemType;
    public float despawnTimer = 60f;
    public bool doingTimer = true;

    private void Update()
    {
        if (doingTimer == true)
        {
            despawnTimer -= Time.deltaTime;
            if (despawnTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
