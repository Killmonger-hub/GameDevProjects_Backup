using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceshipControls : MonoBehaviour
{
    public GameManager gm;
    public GameObject newHighScorePanel;
    public Text highScoreListText;
    public Text scoreText;
    public Text livesText;

    public Alien alien;
    public GameObject gameOverPanel;
    public GameObject explode;
    public GameObject bullet;
    public Transform firePoint;

    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public AudioSource explodeAudio;
    public AudioSource hyperspaceAudio;
    public AudioSource respawnAudio;
    public Rigidbody2D rb;

    public float bulletForce;
    public float deathForce;
    public float thrust;
    public float turnThrust;
    public float thrustVel;

    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;

    public InputField highScoreInput;
    private float thrustInput;
    private float turnInput;
    private bool hyperspace;
    public int score;
    public int lives;

    public Color inColor;
    public Color normalColor;

    void Start()
    {
        score = 0;
        hyperspace = false;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start Menu");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);

            Destroy(newBullet, 3f);
        }

        if (Input.GetButtonDown("Hyperspace") && !hyperspace)
        {
            hyperspaceAudio.Play();
            hyperspace = true;
            spriteRenderer.enabled = false;
            collider.enabled = false;
            Invoke("Hyperspace", 1f);
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
        rb.AddRelativeForce(Vector2.up * thrustVel);
    }

    void ScorePoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score;
    }

    void Respawn()
    {
        respawnAudio.Play();
        transform.position = Vector2.zero;

        spriteRenderer.enabled = true;
        spriteRenderer.enabled = true;
        spriteRenderer.color = inColor;

        Invoke("Invulnerable", 3f);
    }

    void Invulnerable()
    {
        collider.enabled = true;
        spriteRenderer.color = normalColor;
    }

    void Hyperspace()
    {
        Vector2 newPos = new Vector2(Random.Range(-12.16f, 12.16f), Random.Range(-8.35f, 8.35f));
        transform.position = newPos;

        spriteRenderer.enabled = true;
        collider.enabled = true;
        hyperspace = false;
    }

    void LoseLife()
    {
        lives--;

        spriteRenderer.enabled = false;
        collider.enabled = false;

        GameObject newExplode = Instantiate(explode, transform.position, transform.rotation);
        Destroy(newExplode, 3f);
        Invoke("Respawn", 3f);

        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.relativeVelocity.magnitude);
       
        if(col.relativeVelocity.magnitude > deathForce)
        {
            LoseLife();
        }
        else
        {
            explodeAudio.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Beam"))
        {
            LoseLife();
        }      
    }

    void GameOver()
    {
        GetComponent<SpaceshipControls>().enabled = false;
        CancelInvoke();

        if (gm.CheckForHighScore(score))
        {
            newHighScorePanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(true);
            highScoreListText.text = "HIGH SCORE" + "\n" + "1. " + PlayerPrefs.GetString("HighScoreName") + " - " + PlayerPrefs.GetInt("HighScore");
        }
    }

    public void HighScoreInput()
    {
        string newInput = highScoreInput.text;
        Debug.Log(newInput);

        newHighScorePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        PlayerPrefs.SetString("HighScoreName", newInput);
        PlayerPrefs.SetInt("HighScore", score);
        highScoreListText.text = "HIGH SCORE" + "\n" + "1. " + PlayerPrefs.GetString("HighScoreName") + " - " + PlayerPrefs.GetInt("HighScore");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}