﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rb2d;
	private int positionOnLane = 0;

	public float speed = 10f;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("right")) {
			if (positionOnLane < GameController.instance.nbLane-1) 
				positionOnLane++;
		} else if (Input.GetKeyDown ("left")) {
			if (positionOnLane > 0) 
				positionOnLane--;
		}
		float sizeOfLane = GameController.instance.sizeOfLane;
		float offset = (sizeOfLane / 2f);
		float newXPosition = GameController.instance.leftPosition + (sizeOfLane * (positionOnLane + 1) - offset);
		rb2d.transform.position = Vector3.Lerp(rb2d.transform.position, new Vector2(newXPosition, rb2d.transform.position.y), speed * Time.deltaTime);
	}
}
