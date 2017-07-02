using System.Collections;
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

    public GameObject parent;
    
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
        print(positionOnLane);
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
		parent.transform.position = Vector3.Lerp(parent.transform.position, new Vector3(newXPosition, parent.transform.position.y, parent.transform.position.z), horizontalSpeed * Time.deltaTime);

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
        anim.SetInteger("State", 0);
    }
    void IAnimable.State1()
    {
        anim.SetInteger("State", 1);
    }
    void IAnimable.State2()
    {
        anim.SetInteger("State", 2);
    }
    void IAnimable.State3()
    {
        anim.SetInteger("State", 3);
    }
    void IAnimable.AnimTurbo()
    {
        anim.SetInteger("State", 4);
    }

    void IAnimable.AnimMoveLeft()
    {
        anim.SetTrigger("MoveLeft");
    }

    void IAnimable.AnimMoveRight()
    {
        anim.SetTrigger("MoveRight");
    }
    void IAnimable.Explode()
    {
        anim.SetTrigger("Explode");
    }

}
