using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour, IRestartable
{
    private Text score;
    private float time;
	// Use this for initialization
	void Start ()
	{
	    time = 0;
	    score = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    time += Time.deltaTime;
		score.text = "Score \r " + Math.Round(time, 3);
	}

    void IRestartable.Restart(GameController controller)
    {
        time = 0;
    }

    void IRestartable.Stop(GameController controller)
    {
        
    }
}
