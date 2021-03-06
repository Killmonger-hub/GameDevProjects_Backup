﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numOfAsteroids;
    public int levelNum = 1;

    public GameObject asteroid;
    public Alien alien;

    public void UpdateNumOfAsteroids(int change)
    {
        numOfAsteroids += change;
        if (numOfAsteroids <= 0)
        {
            Invoke("StartNewLevel", 3f);
        }
    }

    void StartNewLevel()
    {
        levelNum++;

        for (int i = 0; i < levelNum * 2; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-15.9f, 15.9f), 12.4f);
            Instantiate(asteroid, spawnPos, Quaternion.identity);
            numOfAsteroids++;
        }
        alien.NewLevel();
    }

    public bool CheckForHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (score > highScore)
        {
            return true;
        }
        return false;
    }
}