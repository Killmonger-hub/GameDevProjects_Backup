using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public float damage = 10;
    public LayerMask whatToHit;

    public Transform bulletTrail;
    public Transform muzzleFlash;

    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

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

        if(Time.time>=timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        Debug.DrawLine(firePointPos, (mousePos - firePointPos) * 100, Color.cyan);
        if(hit.collider != null)
        {
            Debug.DrawLine(firePointPos, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");
        }
    }

    void Effect()
    {
        Instantiate(bulletTrail, firePoint.position, firePoint.rotation);
        Transform clone = (Transform)Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}
