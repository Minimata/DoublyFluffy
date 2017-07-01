
public class ToState4 : AkTriggerBase
{
    public void Activate()
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(null);
        }
    }
}
