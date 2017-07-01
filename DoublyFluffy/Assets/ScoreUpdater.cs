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
	    score.text = "Score \r " + time;
	}

    void IRestartable.Restart(GameController controller)
    {
        Debug.Log("Restart from ScoreUpdater");
        time = 0;
    }
}
