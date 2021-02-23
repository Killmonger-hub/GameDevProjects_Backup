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
	private float currentTime;
	private float lastLapTime;
	private float bestLapTime;

	void Update()
	{
		if (UpdateUIForPlayer == null) 
			return;
		if(UpdateUIForPlayer.currentLap != currentLap)
		{
			currentLap = UpdateUIForPlayer.currentLap;
			UITextCurrentLap.text = "LAP: {currentLap}";
		}
	}
}
