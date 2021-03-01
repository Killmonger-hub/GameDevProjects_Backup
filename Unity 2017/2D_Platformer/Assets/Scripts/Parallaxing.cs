using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallaxing : MonoBehaviour
{
    // Array of all back-foregrounds to be parallaxed
    public Transform[] backgrounds;

    // Proportion of the cam's movement to move the backgrounds by
    private float[] parallaxScales;

    // How smooth parallax is going to be. Make sure to set this above 0
    public float smoothing = 1f;

    // Reference to main cam's transform
    private Transform cam;

    // Pos of cam in previous frame
    private Vector3 previousCamPos;

	// Is called before Start(). Great for references.
	void Awake ()
    {
		// Set up camera the reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start ()
    {
		// Previous frame had current frame's cam pos
		previousCamPos = cam.position;

		// Assigning coresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update ()
    {

		// For each background
		for (int i = 0; i < backgrounds.Length; i++)
        {
			// Parallax is opposite of the cam movement because previous frame multiplied by scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// Set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// Create a target position which is the background's current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// Fade between current pos and target pos using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		// Set the previousCamPos to the cam pos at end of frame
		previousCamPos = cam.position;
	}
}
