using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipControls : MonoBehaviour
{
    public Transform firePoint;
    public GameObject explode;
    public GameObject bullet;
    public AudioSource audio;

    public float bulletForce;
    public float deathForce;
    public float thrust;
    public float turnThrust;

    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;

    private float thrustInput;
    private float turnInput;
    public int score;
    public int lives;

    public Text scoreText;
    public Text livesText;
    public Color inColor;
    public Color normalColor;

    void Start()
    {
        score = 0;

        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);

            Destroy(newBullet, 3f);
        }

        transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turnThrust);
        Vector2 newPos = transform.position;
        if(transform.position.y > screenTop)
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

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, thrustInput * thrust * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }

    void ScorePoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score;
    }

    void Respawn()
    {
        transform.position = Vector2.zero;

        GetComponent<SpaceshipControls>().enabled = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = inColor;

        Invoke("Invulnerable", 3f);
        
    }

    void Invulnerable()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = normalColor;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.relativeVelocity.magnitude);
       
        if(col.relativeVelocity.magnitude > deathForce)
        {
            lives--;
            GetComponent<SpaceshipControls>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            GameObject newExplode = Instantiate(explode, transform.position, transform.rotation);
            Destroy(newExplode, 3f);
            Invoke("Respawn", 3f);

            livesText.text = "Lives: " + lives;
            if(lives <= 0)
            {
                
            }
        }
        else
        {
            audio.Play();
        }
    }
}