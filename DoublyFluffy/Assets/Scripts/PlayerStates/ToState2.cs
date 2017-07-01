public class ToState2 : AkTriggerBase
{
    public void Activate()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}