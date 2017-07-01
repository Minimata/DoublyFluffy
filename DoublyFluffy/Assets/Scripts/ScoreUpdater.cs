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
		StartCoroutine (RunTimer ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

    void IRestartable.Restart(GameController controller)
    {
        time = 0;
		StartCoroutine (RunTimer ());
    }

    void IRestartable.Stop(GameController controller)
    {
		StopCoroutine (RunTimer ());
    }

	private IEnumerator RunTimer(){
		while (true) {
			yield return new WaitForSeconds(0.001f);
			time += 0.001f;
			print ("Time" + time);
			score.text = "Score \r " + Math.Round(time*10f, 3);
		}
	}
}
