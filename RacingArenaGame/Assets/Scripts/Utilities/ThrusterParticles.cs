using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterParticles : MonoBehaviour
{
    public ParticleSystem[] systems;
    public GameObject vehicle;
    public BaseVehicle stats;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (systems != null)
        {
            for (int i=0; i < systems.Length; i++)
            {
                var main = systems[i].main;
                main.startSpeed = 1 + vehicle.GetComponent<Rigidbody>().velocity.magnitude
                    / (stats.BaseTopSpeed + ((stats.BaseTopSpeed * stats.PercentTopSpeedPerMod) * stats.ModTopSpeed));
            }
        }
    }
}
