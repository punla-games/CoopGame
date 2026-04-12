public class CoffeeMachineInteractable:BaseInteractable
{
    public override bool CanInteract(Player player)
    {
        return player.item?.Def.Id=="cup";
    }

    public override float GetInteractDuration(Player player)
    {
        return 3f;
    }

    public override string GetInteractText(Player player)
    {
        if(player.item?.Def.Id=="coffee")
            return "You already have a cup of coffee.";
        else if(player.item?.Def.Id!="cup")
            return "You need a cup to make coffee.";
        else
            return $"Press \"F\" to make coffee.";
    }

    public override void OnInteract(Player player)
    {
        player.item=ItemDatabase.Create("coffee");
    }
}
