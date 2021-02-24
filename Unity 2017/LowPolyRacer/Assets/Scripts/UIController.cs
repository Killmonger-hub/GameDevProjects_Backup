using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{
	public GameObject UIRacePanel;

	public Text UITextCurrentLap;
	public Text UITextCurrentTime;
	public Text UITextLastLapTime;
	public Text UITextBestLapTime;
	public Text UITextCurrentLapNo;
	public Text UITextCurrentTimeNo;
	public Text UITextLastLapNo;
	public Text UITextBestLapNo;

	public Player UpdateUIForPlayer;

	private int currentLap;
	private float currentLapTime;
	private float lastLapTime;
	private float bestLapTime;
	float startTime;

	void Start()
	{
		startTime = Time.time;
	}

	void Update()
	{
		float time = Time.time - startTime;
		string min = ((int)time / 60).ToString ();
		string sec = (time % 60).ToString ("f2");

		if (UpdateUIForPlayer == null) 
			return;
		
		if(UpdateUIForPlayer.currentLap != currentLap)
		{
			currentLap = UpdateUIForPlayer.currentLap;
			UITextCurrentLapNo.text = currentLap.ToString();
		}

		if(UpdateUIForPlayer.currentLapTime != currentLapTime)
		{
			currentLapTime = UpdateUIForPlayer.currentLapTime;
			UITextCurrentTimeNo.text = min + ":" + sec;
		}

		if(UpdateUIForPlayer.lastLapTime != lastLapTime)
		{
			lastLapTime = UpdateUIForPlayer.lastLapTime;
			UITextLastLapNo.text = min + ":" + sec;
		}

		if(UpdateUIForPlayer.bestLapTime != bestLapTime)
		{
			bestLapTime = UpdateUIForPlayer.bestLapTime;
			UITextBestLapNo.text = min + ":" + sec;
		}
	}
}
