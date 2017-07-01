using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IRestartable {

	private Rigidbody2D rb2d;
	[SerializeField] private int positionOnLane = 0;
    [SerializeField] private int defaultPositionOnLane = 2;

    public float horizontalSpeed = 10f;

    public GameObject juiciness;
    private JuicinessController juicy;

    private int juiceIncrement;

    // Use this for initialization
    void Start ()
    {
        juicy = juiciness.GetComponent<JuicinessController>();

        rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
        //Inputs and moving behavior
		if (Input.GetKeyDown ("right")) {
			if (positionOnLane < GameController.instance.nbLanes-1) 
				positionOnLane++;
		} else if (Input.GetKeyDown ("left")) {
			if (positionOnLane > 0) 
				positionOnLane--;
		}
		float sizeOfLane = GameController.instance.sizeOfLane;
		float offset = (sizeOfLane / 2f);
		float newXPosition = GameController.instance.leftPosition + (sizeOfLane * (positionOnLane + 1) - offset);
		rb2d.transform.position = Vector3.Lerp(rb2d.transform.position, new Vector3(newXPosition, rb2d.transform.position.y, transform.position.z), horizontalSpeed * Time.deltaTime);

        //Juice control
	    juicy.juice += juiceIncrement;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LaneBlue") juiceIncrement = -1;
        else if (other.tag == "LaneYellow") juiceIncrement = 1;
    }

    void IRestartable.Restart(GameController controller)
    {
        Debug.Log("Restart from PlayerController");
        positionOnLane = defaultPositionOnLane;
        juiceIncrement = 0;
    }
}
