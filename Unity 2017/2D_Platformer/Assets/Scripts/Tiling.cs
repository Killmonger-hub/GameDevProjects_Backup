﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour
{
    // Offset so that we don't get any weird errors
    public int offsetX = 2;	

	// These are used for checking if we need to instantiate stuff
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

    // Used if the object is not tilable
    public bool reverseScale = false;

    // Width of our element
    private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	void Awake ()
    {
		cam = Camera.main;
		myTransform = transform;
	}

	void Start ()
    {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x * myTransform.localScale.x;
        if(spriteWidth < 0)
        {
            spriteWidth = spriteWidth * -1;
        }
	}
	
	void Update ()
    {
		// Does it still need buddies? If not do nothing
		if (hasALeftBuddy == false || hasARightBuddy == false)
        {
			// Calculate the cameras extend (half the width) of what the camera can see in world coordinates
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

			// Calculate the x position where the camera can see the edge of the sprite (element)
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

			// Checking if we can see the edge of the element and then calling MakeNewBuddy if we can
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBuddy (1);
				hasARightBuddy = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
				MakeNewBuddy (-1);
				hasALeftBuddy = true;
			}
		}
	}

	// A func that creates buddy onside required
	void MakeNewBuddy (int rightOrLeft)
    {
		// Calculating the new position for our new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		// Instantating new buddy and storing it in variable
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

		// If not tilable, reverse x size of object to get rid of seams
		if (reverseScale == true)
        {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;
		if (rightOrLeft > 0)
        {
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}
		else
        {
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}
}
