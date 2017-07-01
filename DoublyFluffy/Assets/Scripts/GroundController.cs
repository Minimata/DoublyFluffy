using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour, IRestartable
{

    public float speed = 1.0f;
    public int nbLanes = 5;

    public GameObject juiciness;
    private JuicinessController juicy;

    // Use this for initialization
    void Start ()
    {
        juicy = juiciness.GetComponent<JuicinessController>();
    }
	
	// Update is called once per frame
	void Update ()
	{
        Vector3 move = Vector3.down * speed * juicy.juice / juicy.maxJuice;
	    transform.position += move;
	}

    void IRestartable.Restart(GameController controller)
    {
        Debug.Log("Restart from GroundController");
        transform.position = new Vector3(0, 10, 0);
    }
}
