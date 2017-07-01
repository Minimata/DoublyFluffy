using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    [SerializeField] private float m_speed = 0.01f;
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
        Vector3 move = Vector3.down * m_speed;
	    transform.position += move;
	}
}
