public class Item
{
    private ItemDefinition definition { get; }

    public Item(ItemDefinition definition)
    {
        this.definition=definition;
    }

    public string Id => definition.Id;
    public virtual string Title => definition.Title;
}
