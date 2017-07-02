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
    [SerializeField]
    private float state0Limit = 150.0f;
    [SerializeField]
    private float state1Limit = 250.0f;
    [SerializeField]
    private float state2Limit = 350.0f;
    [SerializeField]
    private float state3Limit = 450.0f;

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

    private bool isTurbo = false;
    private float turboTime;
    [SerializeField] private float defaultTurboTime = 8.0f;


    // Use this for initialization
    void Start ()
    {
        turboTime = defaultTurboTime;

        yellowIncrement = defaultYellowIncrement;
        blueIncrement = defaultBlueIncrement;

        juice = defaultJuice;
        oldState = currentState = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (juice < state0Limit) currentState = 0;
        else if (juice >= state0Limit && juice < state1Limit) currentState = 1;
        else if (juice >= state1Limit && juice < state2Limit) currentState = 2;
        else if (juice >= state2Limit && juice < state3Limit) currentState = 3;
        else if (juice >= state3Limit) currentState = 4;

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

	    if (oldState < currentState) AkSoundEngine.PostEvent("State_Up", gameObject);
        else if (oldState > currentState) AkSoundEngine.PostEvent("State_down", gameObject);

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
                isTurbo = true;
                AkSoundEngine.PostEvent("ToState4", null);
                break;

        }

	    oldState = currentState;

	}

    //Called in player update if level not finished
    public void Increment()
    {
        if (!isTurbo)
        {
            turboTime = defaultTurboTime;
            juice += increment * isBlue;
        }
        else
        {
            GameController.instance.Turbo();
            juice = maxJuice;
            turboTime -= Time.deltaTime;
            print(turboTime);
            if (turboTime < 0)
            {
                if (isBlue > 0)
                {
                    GameController.instance.GameOver();
                    GameController.instance.DefeatHigh();
                }
                else
                {
                    juice = (state2Limit + state3Limit)/2.0f;
                }
                isTurbo = false;
            }
        }
    }

    void IRestartable.Restart(GameController controller)
    {
        isBlue = 0;
        isTurbo = false;
        turboTime = defaultTurboTime;
        blueIncrement = defaultBlueIncrement;
        yellowIncrement = defaultYellowIncrement;
        juice = defaultJuice;
    }

    void IRestartable.Stop(GameController controller)
    {
        
    }
}
