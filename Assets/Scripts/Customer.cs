using UnityEngine;
using UnityEngine.AI;

public class Customer:Interactable
{
    public NavMeshAgent agent;

    public enum State
    {
        ENTERING,
        WAITING,
        LEAVING,
    }

    public State state = State.ENTERING;
    public Vector3 enterPos = Vector3.zero;
    public Vector3 leavePos = Vector3.zero;

    public void Update()
    {
        if(state==State.ENTERING)
        {
            agent.destination=enterPos;
            if(HasReachedDestination())
            {
                state=State.WAITING;
            }
        }
        else if(state==State.WAITING)
        {
            // waiting for coffee.
        }
        else if(state==State.LEAVING)
        {
            agent.destination=leavePos;
            if(HasReachedDestination())
            {
                Destroy(gameObject);
            }
        }
    }

    public override bool CanInteract(Player player)
    {
        return state==State.WAITING && player.HeldItem is CupItem;
    }
    public override float GetInteractDuration(Player player)
    {
        return 0.5f;
    }
    public override string GetInteractText(Player player)
    {
        if(player.HeldItem is CupItem)
            return $"Deliver coffee.";

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

            // consume.
            player.DropItem();

            // state.
            state=State.LEAVING;
            return;
        }
    }

    bool HasReachedDestination()
    {
        if(agent.pathPending)
            return false;

        if(agent.remainingDistance>agent.stoppingDistance)
            return false;

        if(agent.hasPath&&agent.velocity.sqrMagnitude>0f)
            return false;

        return true;
    }
}
