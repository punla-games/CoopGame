public static class Extensions
{
    public static int ToInt(this bool boolean)
    {
        return boolean ? 1 : 0;
    }
}