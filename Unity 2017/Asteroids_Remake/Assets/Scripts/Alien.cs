using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject explosion;
    public GameObject bullet;
    public Transform startPos;
    public Transform player;

    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public Rigidbody2D rb;

    public int currentLevel = 0;
    public int points;
    public bool disabled;
    public Vector2 dir;

    public float timeBeforeSpawning;
    public float speed;
    public float bulletSpeed;
    public float shotDelay;
    public float lastTimeShot = 0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        NewLevel();
    }

    void Update()
    {
        if (disabled)
        {
            return;
        }

        if (Time.time > lastTimeShot+shotDelay)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject newBullet = Instantiate(bullet, transform.position, q);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
            Destroy(newBullet, 5f);
            lastTimeShot = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (disabled)
            return;

        dir = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    public void NewLevel()
    {
        Disable();
        currentLevel++;

        timeBeforeSpawning = Random.Range(5f, 20f);
        Invoke("Enable", timeBeforeSpawning);

        speed = currentLevel;
        bulletSpeed = 250 * currentLevel;
        points = 50 * currentLevel;
    }

    void Enable()
    {
        transform.position = startPos.position;
        collider.enabled = true;
        spriteRenderer.enabled = true;
        disabled = false;
    }

    public void Disable()
    {
        collider.enabled = false;
        spriteRenderer.enabled = false;
        disabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            player.SendMessage("ScorePoints", points);

            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            Disable();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            Disable();
        }
    }
}