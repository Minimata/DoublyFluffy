using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicinessController : MonoBehaviour
{

    [HideInInspector] public int juicy;
    [HideInInspector] public int currentState;

    [SerializeField] private int defaultJuice = 100;
    [SerializeField] private int maxJuice = 1000;
    [SerializeField] private int nbStates = 10;


    // Use this for initialization
    void Start ()
    {
        juicy = defaultJuice;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    int stateWidth = maxJuice / nbStates;
	    currentState = juicy / stateWidth;

        if(juicy < 0) Debug.Log("Juice too low.");
        else if(juicy > maxJuice) Debug.Log("JUICE TOO HIGH OMG !!!");
	}
}
