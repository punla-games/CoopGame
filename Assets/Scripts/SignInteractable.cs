public class SignInteractable:BaseInteractable
{
    public string message = "";

    public override string GetInteractText(Player player)
    {
        return message;
    }
}