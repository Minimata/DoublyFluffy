using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;
	[HideInInspector] public float sizeOfLane;
    [HideInInspector] public float leftPosition;
	public int nbLane = 5;

    public GameObject ground;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		float xMax = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)).x;
		leftPosition = -xMax;
		sizeOfLane = (xMax*2)/nbLane;
	}

	


}
