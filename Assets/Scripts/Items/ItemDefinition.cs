using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu]
public class ItemDefinition:ScriptableObject
{
    public string Id => name;

    public string Title => name.ToTitleCase();

    public GameObject HeldView = null;

    public virtual Item CreateInstance()
    {
        return new Item(this);
    }
}
