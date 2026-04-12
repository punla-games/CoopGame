public class CustomerInteractable:BaseInteractable
{
    public override float GetInteractDuration(Player player)
    {
        if(player.item=="cup of coffee")
            return 0.5f;

        return 0f;
    }

    public override string GetInteractText(Player player)
    {
        if(player.item=="cup of coffee")
            return "Press \"F\" to deliver a cup of coffee.";

        return $"\"One cup of coffee please!\"";
    }

    public override void OnInteract(Player player)
    {
        if(player.item=="cup of coffee")
        {
            player.item="empty";
            GameManager.Get.money+=750;
            return;
        }
    }
}