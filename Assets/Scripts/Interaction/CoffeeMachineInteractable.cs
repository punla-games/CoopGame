public class CoffeeMachineInteractable:BaseInteractable
{
    public override float GetInteractDuration(Player player)
    {
        return 0.5f;
    }

    public override string GetInteractText(Player player)
    {
        return $"Press \"F\" to get coffee.";
    }

    public override void OnInteract(Player player)
    {
        var cupItem = ItemDatabase.Create("cup") as CupItem;
        cupItem.espresso=2;
        cupItem.water=3;
        player.HoldItem(cupItem);
    }
}
