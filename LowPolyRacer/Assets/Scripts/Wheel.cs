using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour 
{
	public bool steer;
	public bool invertSteer;
	public bool power;

	public float SteerAngle{ get; set;}
	public float Torque{ get; set;}

	private WheelCollider wheelCollider;
	private Transform wheelTransform;

	void Start()
	{
		wheelCollider = GetComponentInChildren<WheelCollider> ();
		wheelTransform = GetComponentInChildren<MeshRenderer> ().GetComponent<Transform>();
	}

	void Update()
	{
		Vector3 pos;
		Quaternion rot;

		wheelCollider.GetWorldPose(out pos, out rot);
		wheelTransform.position = pos;
		wheelTransform.rotation = rot;
	}

	void FixedUpdate()
	{
		if (steer) 
		{
			wheelCollider.steerAngle = SteerAngle * (invertSteer ? -1 : 1);
		}

		if (power) 
		{
			wheelCollider.motorTorque = Torque;
		}
	}
}
