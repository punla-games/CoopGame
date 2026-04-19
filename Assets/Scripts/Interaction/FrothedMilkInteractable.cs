public class FrothedMilkInteractable:Interactable
{
    public override bool CanInteract(Player player)
    {
        return player.HeldItem is CupItem;
    }

    public override float GetInteractDuration(Player player)
    {
        return 0.5f;
    }

    public override string GetInteractText(Player player)
    {
        if(player.HeldItem is not CupItem)
            return "You need a cup.";
        else
            return $"Press \"F\" to add frothed milk.";
    }

    public override void OnInteract(Player player)
    {
        (player.HeldItem as CupItem).frothedMilk++;
    }
}
