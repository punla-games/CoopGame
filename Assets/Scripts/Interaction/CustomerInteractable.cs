public class CustomerInteractable:Interactable
{
    public override float GetInteractDuration(Player player)
    {
        if(player.HeldItem is CupItem)
            return 0.5f;

        return 0f;
    }

    public override string GetInteractText(Player player)
    {
        if(player.HeldItem?.Id=="coffee")
            return "Press \"F\" to deliver a cup of coffee.";

        return $"\"One cup of coffee please!\"";
    }

    public override void OnInteract(Player player)
    {
        if(player.HeldItem is CupItem)
        {
            var cup = player.HeldItem as CupItem;

            // recipes.
            if(cup.espresso==2&&cup.water==3&&cup.oatMilk==0&&cup.cocoaPowder==0&&cup.frothedMilk==0)
                GameManager.Get.money+=750;
            if(cup.espresso==2&&cup.water==0&&cup.oatMilk==3&&cup.cocoaPowder==0&&cup.frothedMilk==0)
                GameManager.Get.money+=750;
            if(cup.espresso==2&&cup.water==0&&cup.oatMilk==0&&cup.cocoaPowder==2&&cup.frothedMilk==1)
                GameManager.Get.money+=750;
            if(cup.espresso==1&&cup.water==0&&cup.oatMilk==2&&cup.cocoaPowder==0&&cup.frothedMilk==2)
                GameManager.Get.money+=750;
            if(cup.espresso==3&&cup.water==0&&cup.oatMilk==0&&cup.cocoaPowder==0&&cup.frothedMilk==2)
                GameManager.Get.money+=750;

            // fallback.
            if(cup.espresso>0||cup.water>0||cup.oatMilk>0||cup.cocoaPowder>0||cup.frothedMilk>0)
                GameManager.Get.money+=50;

            player.DropItem();
            return;
        }
    }
}