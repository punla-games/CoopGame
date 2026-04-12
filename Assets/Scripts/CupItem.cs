public class CupItem:Item
{
    public int espresso = 0;
    public int water = 0;
    public int oatMilk = 0;
    public int cocoaPowder = 0;
    public int frothedMilk = 0;

    public CupItem(ItemDefinition definition) : base(definition)
    {
    }

    public override string Title
    {
        get
        {
            // recipes.
            if(espresso==2&&water==3&&oatMilk==0&&cocoaPowder==0&&frothedMilk==0)
                return "calabeeno";
            if(espresso==2&&water==0&&oatMilk==3&&cocoaPowder==0&&frothedMilk==0)
                return "latte";
            if(espresso==2&&water==0&&oatMilk==0&&cocoaPowder==2&&frothedMilk==1)
                return "mochaccino";
            if(espresso==1&&water==0&&oatMilk==2&&cocoaPowder==0&&frothedMilk==2)
                return "cappuccino";
            if(espresso==3&&water==0&&oatMilk==0&&cocoaPowder==0&&frothedMilk==2)
                return "macchiato";

            // fallback.
            if(espresso>0||water>0||oatMilk>0||cocoaPowder>0||frothedMilk>0)
                return $"drink ({espresso},{water},{oatMilk},{cocoaPowder},{frothedMilk})";

            // empty.
            return "cup";
        }
    }
}