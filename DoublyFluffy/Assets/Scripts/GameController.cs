using System;
using System.Collections;
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
    
	private float time;
	private float bestscore;

    public GameObject ground;
    private GroundController gnd;
    public GameObject juiciness;
    private JuicinessController juicy;

    public GameObject[] restartables;
    private IRestartable[] rest;
    public GameObject[] animables;
    private IAnimable[] anim;

    private Animator camerAnim;

    public Text VictoryText;
    public Text GameOverText;
    public Text DefeatLowText;
    public Text DefeatHighText;
	public Text NextLevelText;
	public Text BestScore;
	public Text ScoreText;
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
        camerAnim = GetComponent<Animator>();

        rest = new IRestartable[restartables.Length];
        int i = 0;
        foreach (var comp in restartables)
        {
            rest[i] = comp.GetComponent<IRestartable>();
            i++;
        }

        anim = new IAnimable[animables.Length];
        int j = 0;
        foreach (var comp in animables)
        {
            anim[j] = comp.GetComponent<IAnimable>();
            j++;
        }

        nbLanes = gnd.nbLanes;
        float xMax = nbLanes;
        leftPosition = -(xMax/2);
        sizeOfLane = xMax/nbLanes;
		bestscore = -1;
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
			bestscore = -1;
		}


		float juicyRotationNormalize = ((juicy.juice/((float) juicy.maxJuice))*180)-90;
		Pointer.transform.rotation = Quaternion.Euler (new Vector3(0, 0, -juicyRotationNormalize));

		// Timer
		time += Time.deltaTime;
		ScoreText.text = "Score \r " + Math.Round(time, 3);
	}
    void PlayableUI()
    {
        VictoryText.enabled = false;
        GameOverText.enabled = false;
        DefeatLowText.enabled = false;
        DefeatHighText.enabled = false;
		NextLevelText.enabled = false;
		BestScore.enabled = false;
		LevelText.text = "Level : " + (SceneManager.GetActiveScene ().buildIndex);
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
		time = 0;
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
	    Time.timeScale = 0;

		nextLevel = _nextLevel;

		VictoryText.enabled = true;
		NextLevelText.enabled = true;

		if(bestscore == -1 || time < bestscore)
			bestscore =  (float) Math.Round(time, 3);
		BestScore.text = "Best score : " + bestscore;
		BestScore.enabled = true;

        AkSoundEngine.PostEvent("ToVictory", gameObject);
        Debug.Log("Victory !");
	}

	public void DefeatLow()
	{
	    DefeatLowText.enabled = true;
		if(bestscore > 0)
			BestScore.enabled = true;
        AkSoundEngine.PostEvent("Game_over_too_slow", gameObject);
        Debug.Log("You're going too slow !");
	}

    public void DefeatHigh()
    {
        DefeatHighText.enabled = true;
		if(bestscore > 0)
			BestScore.enabled = true;
        AkSoundEngine.PostEvent("Game_over_too_fast", gameObject);
        Debug.Log("YOUR EPICNESS EXPLODED THE HYPERBEAM-MOTORISED SPACESHIP !!!");
    }

    public void State0()
    {
        camerAnim.SetTrigger("State0");
        foreach (var comp in anim)
        {
            comp.State0();
        }
    }
    public void State1()
    {
        camerAnim.SetTrigger("State1");
        foreach (var comp in anim)
        {
            comp.State1();
        }
    }
    public void State2()
    {
        camerAnim.SetTrigger("State2");
        foreach (var comp in anim)
        {
            comp.State2();
        }
    }
    public void State3()
    {
        camerAnim.SetTrigger("State3");
        foreach (var comp in anim)
        {
            comp.State3();
        }
    }
    public void Turbo()
    {
        camerAnim.SetTrigger("Turbo");
        foreach (var comp in anim)
        {
            comp.AnimTurbo();
        }
    }
    public void MoveLeft()
    {

        camerAnim.SetTrigger("MoveLeft");
        foreach (var comp in anim)
        {
            comp.AnimMoveLeft();
        }
    }
    public void MoveRight()
    {

        camerAnim.SetTrigger("MoveRight");
        foreach (var comp in anim)
        {
            comp.AnimMoveRight();
        }
    }

}
