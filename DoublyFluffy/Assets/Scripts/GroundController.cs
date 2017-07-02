using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour, IRestartable
{
    public float speed = 1.0f;
    public int nbLanes = 5;

    public GameObject juiciness;
    private JuicinessController juicy;
    private float defaultHeight = 0;

    // Use this for initialization
    void Start ()
    {
        defaultHeight = transform.position.y;
		transform.position = new Vector3(0, 10, 0);
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
        speed = 1.0f;
        transform.position = new Vector3(0, defaultHeight, 0);
    }

    void IRestartable.Stop(GameController controller)
    {
        speed = 0.0f;
    }
}
