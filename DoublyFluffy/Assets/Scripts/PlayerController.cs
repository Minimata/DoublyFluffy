using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	private Animator anim;
	[SerializeField] private int positionOnLane = 0;

	public float horizontalSpeed = 10f;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("right")) {
			if (positionOnLane < GameController.instance.nbLanes - 1) {
				positionOnLane++;
				anim.SetTrigger ("MoveRight");
			}
		} else if (Input.GetKeyDown ("left")) {
			if (positionOnLane > 0) {
				positionOnLane--;
				anim.SetTrigger ("MoveLeft");
			}
		
		}
		float sizeOfLane = GameController.instance.sizeOfLane;
		float offset = (sizeOfLane / 2f);
		float newXPosition = GameController.instance.leftPosition + (sizeOfLane * (positionOnLane + 1) - offset);
		rb2d.transform.position = Vector3.Lerp(rb2d.transform.position, new Vector3(newXPosition, rb2d.transform.position.y, transform.position.z), horizontalSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On lane");
        if (other.tag == "LaneBlue") Debug.Log("Blue");
        else if (other.tag == "LaneYellow") Debug.Log("Yellow");
    }
}
