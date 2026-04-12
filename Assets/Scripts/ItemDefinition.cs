using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu]
public class ItemDefinition:ScriptableObject
{
    public string Id => name;
    public string Title => name.ToTitleCase();

    public Item CreateInstance()
    {
        return new Item(this);
    }
}
