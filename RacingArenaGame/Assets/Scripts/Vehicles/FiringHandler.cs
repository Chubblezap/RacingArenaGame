﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringHandler : MonoBehaviour
{
    // universal mechanics
    public GameObject projectileType;
    public GameObject burstParticleType;
    private float fireRate;
    private float rofTimer = 0;
    public bool active = false;

    // special conditions
    public bool needsInit = false;

    // ammo
    public bool hasAmmo = false;
    private int totalAmmo;
    private int currentAmmo = 0;
    private float reloadSpeed;
    private float reloadTimer = 0;

    // charging
    public bool hasCharge = false;
    private float chargeTime;
    private float curChargeTime = 0;

    private void Start()
    {
        if(needsInit)
        {
            GetGunStats();
        }
    }

    private void FixedUpdate()
    {
        // non-charge weapons
        if (!hasCharge)
        {
            if (rofTimer > 0)
            {
                rofTimer -= Time.deltaTime;
                if (rofTimer <= 0)
                {
                    rofTimer = 0;
                }
            }
            if (active && rofTimer == 0)
            {
                Fire();
            }
        }
        // charge weapons
        else
        {
            if (active)  // active
            {
                if (curChargeTime < chargeTime)
                {
                    curChargeTime += Time.deltaTime;
                }
                else
                {
                    curChargeTime = chargeTime;
                }
            }
            else // inactive
            {
                if (curChargeTime >= chargeTime)
                {
                    Fire();
                }
                else
                {
                    if (curChargeTime > 0)
                    {
                        curChargeTime -= Time.deltaTime * 2;
                    }
                    else
                    {
                        curChargeTime = 0;
                    }
                }
            }
        }

        // reloading handler
        if (hasAmmo && currentAmmo == 0)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadSpeed)
            {
                reloadTimer = 0;
                currentAmmo = totalAmmo;
            }
        }
    }

    void Fire()
    {
        // gun is ready to fire
        if ((!hasAmmo && !hasCharge) || (hasAmmo && currentAmmo > 0 && !hasCharge) || (hasCharge && curChargeTime == chargeTime && !hasAmmo) || (hasAmmo && currentAmmo > 0 && hasCharge && curChargeTime == chargeTime))
        {
            GameObject firedProjectile = Instantiate(projectileType, transform.position + transform.up * 0.25f, transform.rotation);
            if (burstParticleType != null)
            {
                Instantiate(burstParticleType, transform.position + transform.up * 0.25f, transform.rotation, transform);
            }
            firedProjectile.GetComponent<BasicProjectile>().owner = GetComponent<HeldGun>().owner;
            if(!firedProjectile.GetComponent<BasicProjectile>().sticky)
            {
                firedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * transform.root.GetComponent<Rigidbody>().velocity.magnitude);
            }
            else
            {
                firedProjectile.transform.SetParent(transform);
            }
            rofTimer = fireRate;
            if (hasAmmo)
            {
                currentAmmo -= 1;
            }
            if (hasCharge)
            {
                curChargeTime = 0;
            }
        }
    }

    public float GetBarAmount()
    {
        if (hasAmmo)
        {
            if(currentAmmo > 0)
            {
                return (float)currentAmmo / totalAmmo;
            }
            else
            {
                return reloadTimer / reloadSpeed;
            }
        }
        else if (hasCharge)
        {
            return (float)curChargeTime / chargeTime;
        }
        else
        {
            Debug.Log("Gun has no ammo or charge, something wrong");
            return 0;
        }
    }

    public void GetGunStats()
    {
        string thisgun = GetComponent<HeldGun>().gunType;
        InfoDump statsheet = GameObject.Find("GameController").GetComponent<InfoDump>();
        projectileType = statsheet.GetGunProjectile(thisgun);
        burstParticleType = statsheet.GetGunBurst(thisgun);
        fireRate = statsheet.GetGunFireRate(thisgun);
        totalAmmo = statsheet.GetGunMaxAmmo(thisgun);
        hasAmmo = (totalAmmo == 0 ? false : true);
        reloadSpeed = statsheet.GetGunReloadSpeed(thisgun);
        chargeTime = statsheet.GetGunChargeTime(thisgun);
        hasCharge = (chargeTime == 0 ? false : true);
    }
}
