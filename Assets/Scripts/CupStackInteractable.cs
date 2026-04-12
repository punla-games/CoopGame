public class CupStackInteractable:BaseInteractable
{
    public int limit = 5;

    public int amount = 0;

    public void Awake()
    {
        amount=limit;
    }

    public override bool CanInteract(Player player)
    {
        return amount>0;
    }

    public override float GetInteractDuration(Player player)
    {
        return 0.5f;
    }

    public override string GetInteractText(Player player)
    {
        if(amount==0)
            return "No cups remaining.";

        if(player.item=="cup")
            return "I already have a cup.";

        return $"Press \"F\" to take a cup. ({amount}/{limit})";
    }

    public override void OnInteract(Player player)
    {
        player.item="cup";

        amount--;
    }
}
