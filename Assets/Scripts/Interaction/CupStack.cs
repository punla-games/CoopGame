public class CupStack:BaseInteractable
{
    public override string GetInteractText(Player player)
    {
        return $"Press \"F\" to get a cup.";
    }

    public override void OnInteract(Player player)
    {
        var cupItem = ItemDatabase.Create("cup") as CupItem;
        player.HoldItem(cupItem);
    }
}