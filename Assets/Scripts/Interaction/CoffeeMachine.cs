using UnityEngine;

public class CoffeeMachine:Interactable
{
    public CupItem cup = null;

    public GameObject cupView;

    public void Update()
    {
        cupView.SetActive(cup!=null);
    }

    public override bool CanInteract(Player player)
    {
        if(cup==null&&player.HeldItem is CupItem)
            return true;
        if(cup!=null&&player.HeldItem==null)
            return true;

        return false;
    }

    public override float GetInteractDuration(Player player)
    {
        return 0.5f;
    }

    public override string GetInteractText(Player player)
    {
        if(cup==null&&player.HeldItem is CupItem)
            return "Place cup.";
        if(cup!=null&&player.HeldItem==null)
            return "Take cup.";

        return "";
    }

    public override void OnInteract(Player player)
    {

        if(cup==null&&player.HeldItem is CupItem)
        {
            cup=player.HeldItem as CupItem;
            player.DropItem();
            return;
        }

        if(cup!=null&&player.HeldItem==null)
        {
            cup.espresso++;
            player.HoldItem(cup);
            cup=null;
            return;
        }

        
    }
}
