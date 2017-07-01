using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicinessController : MonoBehaviour
{

    [HideInInspector] public int juice;
    [HideInInspector] public int currentState;

    public int maxJuice = 1000;
    [SerializeField] private int defaultJuice = 100;
    [SerializeField] private int nbStates = 10;


    // Use this for initialization
    void Start ()
    {
        juice = defaultJuice;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    int stateWidth = maxJuice / nbStates;
	    currentState = juice / stateWidth;

	    if (juice < 0)
	    {
	        Debug.Log("Juice too low.");
	        juice = 0;
	    }
        else if (juice > maxJuice)
	    {
	        Debug.Log("JUICE TOO HIGH OMG !!!");
	        juice = maxJuice;
	    }
	}
}
