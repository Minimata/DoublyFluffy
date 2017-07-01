
public class ToState1 : AkTriggerBase
{
    public void Activate()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}