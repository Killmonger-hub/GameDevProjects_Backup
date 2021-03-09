using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameManager gm;
    public GameObject player;

    public GameObject explosion;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public Rigidbody2D rb;

    public float maxThrust;
    public float maxTorque;

    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;

    public int asteroidSize;
    public int points;


    void Start()
    {
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);

        player = GameObject.FindWithTag("Player");
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    void Update()
    {
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (asteroidSize == 3)
            {
                Instantiate(asteroidMedium, transform.position, transform.rotation);
                Instantiate(asteroidMedium, transform.position, transform.rotation);

                gm.UpdateNumOfAsteroids(1);
            }
            else if (asteroidSize == 2)
            {
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);

                gm.UpdateNumOfAsteroids(1);
            }
            else if (asteroidSize == 1)
            {
                gm.UpdateNumOfAsteroids(-1);
            }
            player.SendMessage("ScorePoints", points);

            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);

            Destroy(gameObject);
        }
    }
}