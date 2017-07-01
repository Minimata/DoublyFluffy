using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance;
	[HideInInspector] public float sizeOfLane;
    [HideInInspector] public float leftPosition;
	public int nbLanes;

    public float timeSinceRestart;
    public GameObject ground;
    public GameObject juiciness;
    private JuicinessController juicy;

    public GameObject[] restartables;
    private IRestartable[] rest;

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
        juicy = juiciness.GetComponent<JuicinessController>();

        rest = new IRestartable[restartables.Length];
        int i = 0;
        foreach (var comp in restartables)
        {
            rest[i] = comp.GetComponent<IRestartable>();
            i++;
        }

        nbLanes = ground.GetComponent<GroundController>().nbLanes;
        float xMax = nbLanes;
        leftPosition = -(xMax/2);
        sizeOfLane = xMax/nbLanes;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) Restart();
    }

    void Restart()
    {
        Debug.Log("Restart from GameController");
        timeSinceRestart = Time.time;
        foreach (var comp in rest)
        {
            comp.Restart(this);
        }
    }
}
