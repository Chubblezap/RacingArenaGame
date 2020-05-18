using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDump : MonoBehaviour
{
    public Mesh[] gunMeshes;
    public GameObject[] gunProjectiles;
    public float[] gunFireRates;
    public int[] gunMaxAmmo;
    public float[] gunReloadSpeed;
    public float[] gunChargeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Mesh GetGunMesh(string gunType)
    {
        switch (gunType)
        {
            case "Basic Gun":
                return gunMeshes[0];
            case "Flamethrower":
                return gunMeshes[1];
            case "Bomb Cannon":
                return gunMeshes[2];
            case "Mine Layer":
                return gunMeshes[3];
            case "Viral Spiral":
                return gunMeshes[4];
            case "Gearblade Launcher":
                return gunMeshes[5];
            case "Boost Jumper":
                return gunMeshes[6];
            default:
                return null;
        }
    }

    public GameObject GetGunProjectile(string gunType)
    {
        switch (gunType)
        {
            case "Basic Gun":
                return gunProjectiles[0];
            case "Flamethrower":
                return gunProjectiles[1];
            case "Bomb Cannon":
                return gunProjectiles[2];
            case "Mine Layer":
                return gunProjectiles[3];
            case "Viral Spiral":
                return gunProjectiles[4];
            case "Gearblade Launcher":
                return gunProjectiles[5];
            case "Boost Jumper":
                return gunProjectiles[6];
            default:
                return null;
        }
    }

    public float GetGunFireRate(string gunType)
    {
        switch (gunType)
        {
            case "Basic Gun":
                return gunFireRates[0];
            case "Flamethrower":
                return gunFireRates[1];
            case "Bomb Cannon":
                return gunFireRates[2];
            case "Mine Layer":
                return gunFireRates[3];
            case "Viral Spiral":
                return gunFireRates[4];
            case "Gearblade Launcher":
                return gunFireRates[5];
            case "Boost Jumper":
                return gunFireRates[6];
            default:
                return 0;
        }
    }

    public int GetGunMaxAmmo(string gunType)
    {
        switch (gunType)
        {
            case "Basic Gun":
                return gunMaxAmmo[0];
            case "Flamethrower":
                return gunMaxAmmo[1];
            case "Bomb Cannon":
                return gunMaxAmmo[2];
            case "Mine Layer":
                return gunMaxAmmo[3];
            case "Viral Spiral":
                return gunMaxAmmo[4];
            case "Gearblade Launcher":
                return gunMaxAmmo[5];
            case "Boost Jumper":
                return gunMaxAmmo[6];
            default:
                return 0;
        }
    }

    public float GetGunChargeTime(string gunType)
    {
        switch (gunType)
        {
            case "Basic Gun":
                return gunChargeTime[0];
            case "Flamethrower":
                return gunChargeTime[1];
            case "Bomb Cannon":
                return gunChargeTime[2];
            case "Mine Layer":
                return gunChargeTime[3];
            case "Viral Spiral":
                return gunChargeTime[4];
            case "Gearblade Launcher":
                return gunChargeTime[5];
            case "Boost Jumper":
                return gunChargeTime[6];
            default:
                return 0;
        }
    }

    public float GetGunReloadSpeed(string gunType)
    {
        switch (gunType)
        {
            case "Basic Gun":
                return gunReloadSpeed[0];
            case "Flamethrower":
                return gunReloadSpeed[1];
            case "Bomb Cannon":
                return gunReloadSpeed[2];
            case "Mine Layer":
                return gunReloadSpeed[3];
            case "Viral Spiral":
                return gunReloadSpeed[4];
            case "Gearblade Launcher":
                return gunReloadSpeed[5];
            case "Boost Jumper":
                return gunReloadSpeed[6];
            default:
                return 0;
        }
    }
}