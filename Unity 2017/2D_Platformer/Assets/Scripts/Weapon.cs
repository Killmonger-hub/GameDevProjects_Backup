﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public int damage = 10;
    public LayerMask whatToHit;

    public Transform bulletTrail;
    public Transform hitEffect;
    public Transform muzzleFlash;

    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public float camShakeLength = 0.1f;
    public float camShakeAmt = 0.05f;
    CameraShake camShake;

    private float timeToFire = 0;
    Transform firePoint;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No Fire Point available");
        }
    }

    void Start()
    {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
            Debug.LogError("No Camera Shake script found on GM object");
    }

    void Update()
    {
        if (fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePos - firePointPos, 100, whatToHit);

        Debug.DrawLine(firePointPos, (mousePos - firePointPos) * 100, Color.cyan);
        if(hit.collider != null)
        {
            Debug.DrawLine(firePointPos, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DamageEnemy(damage);
                Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePos - firePointPos) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = (Transform)Instantiate(bulletTrail, firePoint.position, firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }

        Destroy(trail.gameObject, 0.04f);

        if (hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitParticles = (Transform)Instantiate(hitEffect, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitParticles.gameObject, 1f);
        }

        Transform clone = (Transform)Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;

        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);

        Destroy(clone.gameObject, 0.02f);

        camShake.Shake(camShakeAmt, camShakeLength);
    }
}