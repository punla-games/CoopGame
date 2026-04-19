public class SignInteractable:Interactable
{
    public string message = "";

    public override string GetInteractText(Player player)
    {
        return message;
    }
}