using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;    // List of BG's to be parallaxed
    private float[] parallaxScales;    // Proportion of camera's movement to move BG's by
    public float smoothing = 1f;       // How smooth the parallax is. Make sure to set this above 0

    private Transform cam;             // Reference to main cam's transform
    private Vector3 previousCamPos;    // Position of cam's pos in the previous frame

    // Is called before Start(). Great for references
    void Awake()
    {
        // Set up cam reference
        cam = Camera.main.transform;
    }

    void Start()
    {
        // The previous frame had the current frames cam pos
        previousCamPos = cam.position;

        // Assigning corresponding parallax Scales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        // For each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Parallax is opp. of cam movement cause previous frame is multiplied by scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // Set a target x pos which is current pos + parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target pos which is background's current pos with its target x pos
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fade between current pos and target pos using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Set previous cam pos to cam's pos at end of frame
        previousCamPos = cam.position;
    }
}
