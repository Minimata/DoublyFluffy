using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicinessController : MonoBehaviour, IRestartable
{

    [HideInInspector] public float juice;
    [HideInInspector] public int currentState;
    private int oldState;

    public int maxJuice = 500;
    [SerializeField] private float defaultJuice = 100;
    [SerializeField] private int nbStates = 5;

    [HideInInspector] public int isBlue = 0;

    [SerializeField]
    private float defaultBlueIncrement = -1;
    [SerializeField]
    private float defaultYellowIncrement = 5;
    [SerializeField]
    private float accelerationFactor = 1.0f;
    [SerializeField]
    private float linearFactor = 1.0f;

    private float increment = 0.0f;
    private float yellowIncrement = 1.0f;
    private float blueIncrement = 1.33f;


    // Use this for initialization
    void Start ()
    {
        yellowIncrement = defaultYellowIncrement;
        blueIncrement = defaultBlueIncrement;

        juice = defaultJuice;
        oldState = currentState = 1;
    }
	
	// Update is called once per frame
	void Update ()
	{
        float stateWidth = maxJuice / nbStates;
	    currentState = Convert.ToInt32(juice/stateWidth);

	    if (juice < 0)
	    {
	        juice = 0;
			GameController.instance.GameOver();
			GameController.instance.DefeatLow();
	    }
        else if (juice > maxJuice)
	    {
	        juice = maxJuice;
			GameController.instance.GameOver();
			GameController.instance.DefeatHigh();
	    }

	    if (oldState < currentState) AkSoundEngine.PostEvent("State_down", gameObject);
        else if (oldState > currentState) AkSoundEngine.PostEvent("State_Up", gameObject);

        if(isBlue < 0)
            increment = juice * accelerationFactor * blueIncrement;
        else if (isBlue > 0)
            increment = juice * accelerationFactor * yellowIncrement;

        switch (currentState)
        {
            case 0:
                AkSoundEngine.PostEvent("ToState0", null);
                increment += linearFactor;
                break;
            case 1:
                AkSoundEngine.PostEvent("ToState1", null);
                break;
            case 2:
                AkSoundEngine.PostEvent("ToState2", null);
                break;
            case 3:
                AkSoundEngine.PostEvent("ToState3", null);
                break;
            case 4:
                AkSoundEngine.PostEvent("ToState4", null);
                break;

        }

	    oldState = currentState;

	}

    public void Increment()
    {
        juice += increment*isBlue;
        print(increment);
    }

    void IRestartable.Restart(GameController controller)
    {
        isBlue = 0;
        blueIncrement = defaultBlueIncrement;
        yellowIncrement = defaultYellowIncrement;
        juice = defaultJuice;
    }

    void IRestartable.Stop(GameController controller)
    {
        
    }
}
