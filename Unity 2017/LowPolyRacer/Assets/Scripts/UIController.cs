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

	public Player UpdateUIForPlayer;

	private int currentLap;
	private float currentLapTime;
	private float lastLapTime;
	private float bestLapTime;

	void Update()
	{
		if (UpdateUIForPlayer == null) 
			return;
		
		if(UpdateUIForPlayer.currentLap != currentLap)
		{
			currentLap = UpdateUIForPlayer.currentLap;
			UITextCurrentLap.text = $"LAP: {currentLap}";
		}

		if(UpdateUIForPlayer.currentLapTime != currentLapTime)
		{
			currentLapTime = UpdateUIForPlayer.currentLapTime;
			UITextCurrentTime.text = $"Time: {(int)currentLapTime / 60}:{(currentLapTime) % 60:00.000}";
		}

		if(UpdateUIForPlayer.lastLapTime != lastLapTime)
		{
			lastLapTime = UpdateUIForPlayer.lastLapTime;
			UITextLastLapTime.text = $"Last: {(int)lastLapTime / 60}:{(lastLapTime) % 60:00.000}";
		}

		if(UpdateUIForPlayer.bestLapTime != bestLapTime)
		{
			bestLapTime = UpdateUIForPlayer.bestLapTime;
			UITextBestLapTime.text = bestLapTime < 1000000 ? $"Best: {(int)bestLapTime / 60}:{(bestLapTime) % 60:00.000}" : "Best: NONE: ";
		}
	}
}
