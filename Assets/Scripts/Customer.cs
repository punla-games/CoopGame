using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Customer:Interactable
{
    public static string[] ORDER_OPTIONS = new string[] { "calabeeno","latte","mochaccino","cappuccino","macchiato" };

    public TMP_Text stateLabel;

    public NavMeshAgent agent;
    public enum State
    {
        ENTERING,
        ORDERING,
        SEATING,
        WAITING,
        LEAVING,
    }

    public State state = State.ENTERING;
    public int queueIndex = -1;
    public Vector3 queuePos = Vector3.zero;
    public string order = "";
    public Vector3 seatPos = Vector3.zero;
    public Vector3 leavePos = Vector3.zero;

    public bool started = false;
    public Player interactor = null;

    public void Update()
    {
        // state machine.
        if(state==State.ENTERING)
        {
            agent.destination=queuePos;
            if(HasReachedDestination()&&queueIndex==0)
            {
                state=State.ORDERING;
                order=ORDER_OPTIONS[Random.Range(0,ORDER_OPTIONS.Length-1)];
            }
        }
        else if(state==State.ORDERING)
        {
            // waiting for dialog.
        }
        else if(state==State.SEATING)
        {
            agent.destination=seatPos;
            if(HasReachedDestination())
            {
                state=State.WAITING;
            }
        }
        else if(state==State.WAITING)
        {
            // waiting for delivery
        }
        else if(state==State.LEAVING)
        {
            agent.destination=leavePos;
            if(HasReachedDestination())
            {
                Destroy(gameObject);
            }
        }

        // interacting.
        bool anyInput = Keyboard.current.fKey.wasPressedThisFrame&&!started;
        if(interactor!=null&&anyInput)
        {
            PlayerHUD.Get.HideDialog();
            interactor.StopInteraction();
            interactor=null;
        }

        if(started)
            started=false;
    }
    public void LateUpdate()
    {
        stateLabel.text=state.ToString();
    }

    public override bool CanInteract(Player player)
    {
        //return state==State.WAITING && player.HeldItem is CupItem;
        return (state==State.ORDERING&&queueIndex==0)||state==State.WAITING;
    }
    public override string GetInteractText(Player player)
    {
        if(state==State.ORDERING)
            return $"Talk.";

        if(state==State.WAITING&&player.HeldItem is CupItem)
            return $"Deliver.";

        return "";
    }
    public override void OnInteract(Player player)
    {
        // dialog.
        if(player.HeldItem is not CupItem)
        {
            interactor=player;
            player.state=Player.State.INTERACT;
            PlayerHUD.Get.ShowDialog($"Customer: One {order} please.");
            started=true;
        }
        // delivery.
        else if(player.HeldItem is CupItem)
        {
            var cup = player.HeldItem as CupItem;

            if(cup.Title==order)
            {
                player.GainMoney(750);
                player.DropItem();
                state=State.LEAVING;
                CustomerQueueManager.Get.Unregister(this);
            }
            else
            {
                interactor=player;
                player.state=Player.State.INTERACT;
                PlayerHUD.Get.ShowDialog($"Customer: I didn't order that.");
                started=true;
            }
        }
    }
    public override void OnEndInteract(Player player)
    {
        if(state==State.ORDERING)
        {
            //state=State.SEATING;
            state=State.WAITING;
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


public class Table:MonoBehaviour
{
}

public class TableManager:SingletonBehaviour<TableManager>
{
}


