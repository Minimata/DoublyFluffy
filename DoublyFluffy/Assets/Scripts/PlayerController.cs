﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IRestartable, IAnimable {

	private Rigidbody2D rb2d;
	private Animator anim;
	[SerializeField] private int positionOnLane = 0;
    [SerializeField] private int defaultPositionOnLane = 2;

    public float horizontalSpeed = 10f;

    public GameObject juiciness;
    private JuicinessController juicy;
    
	private bool levelEnded = false;

    // Use this for initialization
    void Start ()
    {
        juicy = juiciness.GetComponent<JuicinessController>();

        rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {
        //Inputs and moving behavior
		if (Input.GetKeyDown ("right")) {
			if (positionOnLane < GameController.instance.nbLanes - 1) {
				positionOnLane++;
                GameController.instance.MoveRight();
            }
		} else if (Input.GetKeyDown ("left")) {
			if (positionOnLane > 0) {
				positionOnLane--;
                GameController.instance.MoveLeft();
            }
		
		}
		float sizeOfLane = GameController.instance.sizeOfLane;
		float offset = (sizeOfLane / 2f);
		float newXPosition = GameController.instance.leftPosition + (sizeOfLane * (positionOnLane + 1) - offset);
		rb2d.transform.position = Vector3.Lerp(rb2d.transform.position, new Vector3(newXPosition, rb2d.transform.position.y, transform.position.z), horizontalSpeed * Time.deltaTime);

        //Juice control
		if(!levelEnded)
	    	juicy.Increment();
	}

    void OnTriggerStay2D(Collider2D other)
    {
       if (other.tag == "LaneBlue") juicy.isBlue = -1;
       else if (other.tag == "LaneYellow") juicy.isBlue = 1;
    }

    void IRestartable.Restart(GameController controller)
    {
        positionOnLane = defaultPositionOnLane;
		levelEnded = false;
    }

    void IRestartable.Stop(GameController controller)
    {
		levelEnded = true;
    }

    void IAnimable.State0()
    {
        anim.SetTrigger("State0");
    }
    void IAnimable.State1()
    {
        anim.SetTrigger("State1");
    }
    void IAnimable.State2()
    {
        anim.SetTrigger("State2");
    }
    void IAnimable.State3()
    {
        anim.SetTrigger("State3");
    }
    void IAnimable.AnimTurbo()
    {
        anim.SetTrigger("Turbo");
    }

    void IAnimable.AnimMoveLeft()
    {
        anim.SetTrigger("MoveLeft");
    }

    void IAnimable.AnimMoveRight()
    {
        anim.SetTrigger("MoveRight");
    }

}
