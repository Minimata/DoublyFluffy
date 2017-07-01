using UnityEngine;

public class ToState0 : AkTriggerBase
{
    public void Activate()
    {
        Debug.Log("ToState0");
        if (triggerDelegate != null)
        {
            Debug.Log("ToState0 Trigger");
            triggerDelegate(null);
        }
    }
}
