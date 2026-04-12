public class CoffeeMachineInteractable:BaseInteractable
{
    public override bool CanInteract(Player player)
    {
        return player.item=="cup";
    }

    public override float GetInteractDuration(Player player)
    {
        return 3f;
    }

    public override string GetInteractText(Player player)
    {
        if(player.item=="cup of coffee")
            return "You already have a cup of coffee.";
        else if(player.item!="cup")
            return "You need a cup to make coffee.";
        else
            return $"Press \"F\" to make coffee.";
    }

    public override void OnInteract(Player player)
    {
        player.item="cup of coffee";
    }
}
