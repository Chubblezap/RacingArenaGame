using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDump : MonoBehaviour
{
    // gun info
    public Mesh[] gunMeshes;
    public GameObject[] gunProjectiles;
    public GameObject[] gunBurst;
    public float[] gunFireRates;
    public int[] gunMaxAmmo;
    public float[] gunReloadSpeed;
    public float[] gunChargeTime;

    // part info
    public Mesh[] partMeshes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public Mesh GetGunMesh(string gunType)
    {
        switch (gunType)
        {
            case "Allgear Basic":
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
    }*/

    public GameObject GetGunProjectile(string gunType)
    {
        switch (gunType)
        {
            case "Allgear Basic":
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

    public GameObject GetGunBurst(string gunType) // Particles that spawn on fire
    {
        switch (gunType)
        {
            case "Allgear Basic":
                return gunBurst[0];
            case "Flamethrower":
                return gunBurst[1];
            case "Bomb Cannon":
                return gunBurst[2];
            case "Mine Layer":
                return gunBurst[3];
            case "Viral Spiral":
                return gunBurst[4];
            case "Gearblade Launcher":
                return gunBurst[5];
            case "Boost Jumper":
                return gunBurst[6];
            default:
                return null;
        }
    }

    public float GetGunFireRate(string gunType)
    {
        switch (gunType)
        {
            case "Allgear Basic":
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
            case "Allgear Basic":
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
            case "Allgear Basic":
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
            case "Allgear Basic":
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

    public Mesh GetPartMesh(string partName)
    {
        switch (partName)
        {
            case "Allgear Material":
                return partMeshes[0];
            case "Slicing Edge Material":
                return partMeshes[1];
            case "Macrotech Material":
                return partMeshes[2];
            case "Neurolink Material":
                return partMeshes[3];
            case "Mundanium Chunk":
                return partMeshes[4];
            case "Hardite Alloy":
                return partMeshes[5];
            case "Billionvolt Capacitor":
                return partMeshes[6];
            case "Flex Drive":
                return partMeshes[7];
            case "Antimatter Shard":
                return partMeshes[8];
            default:
                return null;
        }
    }
}