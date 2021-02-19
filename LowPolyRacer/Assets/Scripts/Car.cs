using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour 
{
	public Transform centerOfMass;

	public WheelCollider wheelColliderLeftFront;
	public WheelCollider wheelColliderRightFront;
	public WheelCollider wheelColliderLeftBack;
	public WheelCollider wheelColliderRightBack;

	public Transform wheelLeftFront;
	public Transform wheelRightFront;
	public Transform wheelLeftBack;
	public Transform wheelRightBack;

	public float motorTorque = 100f;
	public float maxSteer = 20f;
	private Rigidbody _rigidbody;

	void Start()
	{
		_rigidbody = GetComponent<Rigidbody> ();
		_rigidbody.centerOfMass = centerOfMass.localPosition;
	}

	void FixedUpdate()
	{
		wheelColliderLeftBack.motorTorque = Input.GetAxis ("Vertical") * motorTorque;
		wheelColliderRightBack.motorTorque = Input.GetAxis ("Vertical") * motorTorque;
		wheelColliderLeftFront.steerAngle = Input.GetAxis ("Horizontal") * maxSteer;
		wheelColliderRightFront.steerAngle = Input.GetAxis ("Horizontal") * maxSteer;
	}

	void Update()
	{
		var pos = Vector3.zero;
		var rot = Quaternion.identity;

		wheelColliderLeftFront.GetWorldPose (out pos, out rot);
		wheelLeftFront.position = pos;
		wheelLeftFront.rotation = rot;

		wheelColliderRightFront.GetWorldPose (out pos, out rot);
		wheelLeftFront.position = pos;
		wheelLeftFront.rotation = rot * Quaternion.Euler(0, 180, 0);

		wheelColliderLeftBack.GetWorldPose (out pos, out rot);
		wheelLeftBack.position = pos;
		wheelLeftBack.rotation = rot;

		wheelColliderRightBack.GetWorldPose (out pos, out rot);
		wheelRightBack.position = pos;
		wheelRightBack.rotation = rot * Quaternion.Euler(0, 180, 0);
	}
}
