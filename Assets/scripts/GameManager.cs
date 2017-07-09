using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static float speedGame=1;

    public static void SetSpeedGame(float value)
    {
        speedGame = value;
    }
    public static float GetSpeedGame()
    {
        return speedGame;
    }
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	public void ExitGame()
    {
        Application.Quit();
    }
}
