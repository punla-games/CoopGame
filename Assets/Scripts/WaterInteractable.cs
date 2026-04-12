public class WaterInteractable:BaseInteractable
{
    public override bool CanInteract(Player player)
    {
        return player.item is CupItem;
    }

    public override float GetInteractDuration(Player player)
    {
        return 0.5f;
    }

    public override string GetInteractText(Player player)
    {
        if(player.item is not CupItem)
            return "You need a cup.";
        else
            return $"Press \"F\" to add water.";
    }

    public override void OnInteract(Player player)
    {
        (player.item as CupItem).water++;
    }
}
