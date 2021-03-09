using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject bullet;
    public Transform player;
    public Rigidbody2D rb;

    public Vector2 dir;
    public float speed;
    public float bulletSpeed;
    public float shotDelay;
    public float lastTimeShot = 0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time > lastTimeShot+shotDelay)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject newBullet = Instantiate(bullet, transform.position, q);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
            lastTimeShot = Time.time;
        }
    }

    void FixedUpdate()
    {
        dir = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
