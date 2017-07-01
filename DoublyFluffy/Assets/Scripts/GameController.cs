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

    public GameObject juiciness;
    private JuicinessController juicy;

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

        nbLanes = ground.GetComponent<GroundController>().nbLanes;
        float xMax = nbLanes;
        leftPosition = -(xMax/2);
        sizeOfLane = xMax/nbLanes;
    }

	public void GameOver(){
		ground.GetComponent<GroundController> ().speed = 0f;
		Time.timeScale = 0;
	}

	public void Victory(){
		
	}

	public void Defeat(){
		
	}

}
