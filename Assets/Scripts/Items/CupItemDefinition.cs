using UnityEngine;

[CreateAssetMenu]
public class CupItemDefinition:ItemDefinition
{
    public override Item CreateInstance()
    {
        return new CupItem(this);
    }
}