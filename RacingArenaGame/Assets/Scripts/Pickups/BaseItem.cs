using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public string itemType;
    private float despawnTimer = 60f;

    private void Update()
    {
        despawnTimer -= Time.deltaTime;
        if(despawnTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
