using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour {

	public int nextLevel = 1;

	private GroundController ground;

	void Start(){
		ground = GetComponentInParent<GroundController> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		ground.speed = 0f;
		GameController.instance.GameOver ();
		GameController.instance.Victory();
	}
}
