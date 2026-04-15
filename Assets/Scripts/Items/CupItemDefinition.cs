using UnityEngine;

[CreateAssetMenu]
public class CupItemDefinition:ItemDefinition
{
    public override Item CreateInstance()
    {
        return new CupItem(this);
    }
}

public class HeldItemView:MonoBehaviour
{
    public Item Item { get; private set; }

    public void Init(Item item)
    {
        Item=item;
    }
}

public class CupHeldItemView:HeldItemView
{
    public void Update()
    {
    }
}