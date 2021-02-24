using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	public enum ControlType {HumanInput, AI}
	public ControlType controlType = ControlType.HumanInput;

	public float bestLapTime = Mathf.Infinity;
	public float lastLapTime = 0;

	public float currentLapTime = 0;
	public int currentLap = 0;

	private float lapTimerTimestamp;
	private int lastCheckpointPassed = 0;

	private Transform checkPointsParent;
	private int checkPointCount;

	private int checkPointLayer;
	private CarController carController;

	void Awake()
	{
		checkPointsParent = GameObject.Find ("Checkpoints").transform;
		checkPointCount = checkPointsParent.childCount;
		checkPointLayer = LayerMask.NameToLayer ("Checkpoint");
		carController = GetComponent<CarController> ();
	}

	void StartLap()
	{
		Debug.Log ("Start Lap!");
		currentLap++;
		lastCheckpointPassed = 1;
		lapTimerTimestamp = Time.time;
	}

	void EndLap()
	{
		lastLapTime = Time.time - lapTimerTimestamp;
		bestLapTime = Mathf.Min (lastLapTime, bestLapTime);
		Debug.Log ("End Lap - Lap Time was " + lastLapTime + " seconds");
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer != checkPointLayer) 
		{
			return;
		}

		// If this is checkpoint 1....
		if(collider.gameObject.name == "1")
		{
			// ....and we've completed a lap , end the current lap
			if (lastCheckpointPassed == checkPointCount) 
			{
				EndLap ();
			}

			// If we are on our firstlap, or we've passed the last checkpoint - start a new lap
			if (currentLap == 0 || lastCheckpointPassed == checkPointCount)
			{
				StartLap();
			}
			return;
		}

		// If we've passed the next checkpoint the sequence, update the latest checkpoint
		if(collider.gameObject.name == (lastCheckpointPassed + 1).ToString())
		{
			lastCheckpointPassed++;
		}
	}

	void Update()
	{
		currentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;

		if(controlType == ControlType.HumanInput)
		{
			carController.Steer = GameManager.Instance.inputController.SteerInput;
			carController.Throttle = GameManager.Instance.inputController.ThrottleInput;
		}
	}
}
