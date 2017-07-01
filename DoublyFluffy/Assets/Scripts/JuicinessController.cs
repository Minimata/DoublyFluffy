using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicinessController : MonoBehaviour, IRestartable
{

    [HideInInspector] public int juice;
    [HideInInspector] public int currentState;

    public int maxJuice = 1000;
    [SerializeField] private int defaultJuice = 100;
    [SerializeField] private int nbStates = 10;

    public GameObject music;

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

	    switch (currentState)
        {
            case 0:
                AkSoundEngine.PostEvent("ToState0", null);
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

    }

    void IRestartable.Restart(GameController controller)
    {
        juice = defaultJuice;
    }

    void IRestartable.Stop(GameController controller)
    {
        
    }
}
