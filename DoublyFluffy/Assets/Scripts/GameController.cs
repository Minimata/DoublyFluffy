﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;
	[HideInInspector] public float sizeOfLane;
    [HideInInspector] public float leftPosition;
	public int nbLanes;

	private int nextLevel = -1;
    
    public GameObject ground;
    private GroundController gnd;
    public GameObject juiciness;
    private JuicinessController juicy;

    public GameObject[] restartables;
    private IRestartable[] rest;
    
    public Text VictoryText;
    public Text GameOverText;
    public Text DefeatLowText;
    public Text DefeatHighText;
	public Text NextLevelText;
	public Text LevelText;
	public Image Pointer;

    void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
    void Start()
    {
        PlayableUI();

        juicy = juiciness.GetComponent<JuicinessController>();
        gnd = ground.GetComponent<GroundController>();

        rest = new IRestartable[restartables.Length];
        int i = 0;
        foreach (var comp in restartables)
        {
            rest[i] = comp.GetComponent<IRestartable>();
            i++;
        }

        nbLanes = gnd.nbLanes;
        float xMax = nbLanes;
        leftPosition = -(xMax/2);
        sizeOfLane = xMax/nbLanes;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AkSoundEngine.PostEvent("Restart", gameObject);
			Restart();
        }
        if (Input.GetKeyDown(KeyCode.Q)) Application.Quit();
		if (Input.GetKeyDown (KeyCode.Space) && nextLevel > 0) {
			SceneManager.LoadScene (nextLevel);
            Time.timeScale = 1;
			nextLevel = -1;
		}



		//float currRotation = Pointer.transform.rotation.eulerAngles.z;
		float juicyRotationNormalize = ((juicy.juice/((float) juicy.maxJuice))*180)-90;
	
		print (juicyRotationNormalize);
		Pointer.transform.rotation = Quaternion.Euler (new Vector3(0, 0, -juicyRotationNormalize));
	}
    void PlayableUI()
    {
        VictoryText.enabled = false;
        GameOverText.enabled = false;
        DefeatLowText.enabled = false;
        DefeatHighText.enabled = false;
		NextLevelText.enabled = false;
		LevelText.text = "Level : " + (SceneManager.GetActiveScene ().buildIndex + 1);
    }

    void Restart()
    {
        AkSoundEngine.PostEvent("Restart", gameObject);
        Time.timeScale = 1;
		nextLevel = -1;
        PlayableUI();

        foreach (var comp in rest)
        {
            comp.Restart(this);
        }
    }

	public void GameOver()
    {
		Time.timeScale = 0;
        GameOverText.enabled = true;
        foreach (var comp in rest)
        {
            comp.Stop(this);
        }
    }

	public void Victory(int _nextLevel)
	{
		nextLevel = _nextLevel;
		VictoryText.enabled = true;
		NextLevelText.enabled = true;
        AkSoundEngine.PostEvent("Victory", gameObject);
        Debug.Log("Victory !");
	}

	public void DefeatLow()
	{
	    DefeatLowText.enabled = true;
        AkSoundEngine.PostEvent("Game_over_too_slow", gameObject);
        Debug.Log("You're going too slow !");
	}

    public void DefeatHigh()
    {
        DefeatHighText.enabled = true;
        AkSoundEngine.PostEvent("Game_over_too_fast", gameObject);
        Debug.Log("YOUR EPICNESS EXPLODED THE HYPERBEAM-MOTORISED SPACESHIP !!!");
    }
    
}
