public class Item
{
    public ItemDefinition Def { get; }

    public Item(ItemDefinition definition)
    {
        Def=definition;
    }
}

public class ItemStack
{
    public Item item;
    public int amount;
}

public class DrinkItem:Item
{
    public ItemStack

    public DrinkItem(ItemDefinition definition) : base(definition)
    {
    }
}