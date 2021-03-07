using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipControls : MonoBehaviour
{
    public float rotSpeed = 180f;
    public float bulletForce;
    public float deathForce;

    public Transform firePoint;
    public GameObject bullet;
    public Rigidbody2D rb;
    

    public float thrust;
    public float turnThrust;

    private float thrustInput;
    private float turnInput;

    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;

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
        rb.AddForce(transform.up * thrustInput);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.relativeVelocity.magnitude);
        if(col.relativeVelocity.magnitude > deathForce)
        {
            Debug.Log("u ded");
        }
    }
}
