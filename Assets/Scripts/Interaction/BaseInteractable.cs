using UnityEngine;

public class BaseInteractable:MonoBehaviour
{
    public virtual bool CanInteract(Player player)
    {
        return true;
    }
    
    public virtual float GetInteractDuration(Player player)
    {
        return 0f;
    }

    public virtual string GetInteractText(Player player)
    {
        return "";
    }

    public virtual void OnBeginInteract(Player player)
    {
        // do something at the start of interaction (set animation, state, etc.)
        Debug.Log("OnBeginInteract");
    }
    public virtual void OnInteract(Player player)
    {
        // the fun stuff goes here.
        Debug.Log("OnInteract");
    }
    public virtual void OnEndInteract(Player player)
    {
        // do something at the start of interaction (set animation, state, etc.)
        Debug.Log("OnEndInteract");
    }
}
