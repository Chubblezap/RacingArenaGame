using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpeedAdjust : MonoBehaviour
{
    ParticleSystem.MainModule main;
    // Start is called before the first frame update
    void Start()
    {
        main = GetComponent<ParticleSystem>().main;
        main.startSpeed = 10 + (transform.root.GetComponent<Rigidbody>().transform.InverseTransformDirection(transform.root.GetComponent<Rigidbody>().velocity).z)/2;
    }
}
