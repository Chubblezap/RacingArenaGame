using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterParticles : MonoBehaviour
{
    public GameObject vehicle;
    public BaseVehicle stats;
    public GameObject[] particleObjects;

    // Update is called once per frame
    void FixedUpdate()
    {
        float vehicleZSpeed = vehicle.GetComponent<Rigidbody>().transform.InverseTransformDirection(vehicle.GetComponent<Rigidbody>().velocity).z;
        if(particleObjects != null)
        {
            if (vehicleZSpeed < 0.5f)
            {
                for (int i = 0; i < particleObjects.Length; i++)
                {
                    if (particleObjects[i].GetComponent<ParticleSystem>() != null)
                    {
                        particleObjects[i].GetComponent<ParticleSystem>().Stop();
                    }
                    else
                    {
                        particleObjects[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < particleObjects.Length; i++)
                {
                    if (particleObjects[i].GetComponent<ParticleSystem>() != null)
                    {
                        particleObjects[i].GetComponent<ParticleSystem>().Play();
                        var main = particleObjects[i].GetComponent<ParticleSystem>().main;
                        main.startSpeed = 2 - (vehicleZSpeed) / 2;
                    }
                    else
                    {
                        particleObjects[i].SetActive(true);
                    }
                }
            }
        }
    }
}
