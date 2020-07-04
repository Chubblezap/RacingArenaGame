using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParticle : MonoBehaviour
{
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
