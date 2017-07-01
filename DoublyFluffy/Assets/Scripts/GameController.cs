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

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		nbLanes = ground.GetComponent<GroundController> ().nbLanes;
		float xMax = nbLanes;
		leftPosition = -(xMax/2);
		sizeOfLane = xMax/nbLanes;
	}

	


}
