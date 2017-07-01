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
        if (Input.GetKeyDown(KeyCode.R)) Restart();
        if (Input.GetKeyDown(KeyCode.Q)) Application.Quit();
    }

    void PlayableUI()
    {
        VictoryText.enabled = false;
        GameOverText.enabled = false;
        DefeatLowText.enabled = false;
        DefeatHighText.enabled = false;
    }

    void Restart()
    {
        Time.timeScale = 1;
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

	public void Victory()
	{
	    VictoryText.enabled = true;
		Debug.Log("Victory !");
	}

	public void DefeatLow()
	{
	    DefeatLowText.enabled = true;
		Debug.Log("You're going too slow !");
	}

    public void DefeatHigh()
    {
        DefeatHighText.enabled = true;
        Debug.Log("YOUR EPICNESS EXPLODED THE HYPERBEAM-MOTORISED SPACESHIP !!!");
    }
    
}
