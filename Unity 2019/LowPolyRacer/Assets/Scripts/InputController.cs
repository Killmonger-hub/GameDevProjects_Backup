using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour 
{
	public string inputSteerAxis = "Horizontal";
	public string inputThrottleAxis = "Vertical";

	public float ThrottleInput{ get; private set;}
	public float SteerInput{ get; private set;}

	void Update()
	{
		SteerInput = Input.GetAxis (inputSteerAxis);
		ThrottleInput = Input.GetAxis (inputThrottleAxis);

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("Menu");
		}
	}
}
