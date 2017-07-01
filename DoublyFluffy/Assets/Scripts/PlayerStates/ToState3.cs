
public class ToState3 : AkTriggerBase
{
    public void Activate()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
