namespace CallSignCommon.ExtensionMethods;
public static class CommonExtMethods
{
    public static bool IsNullOrEmpty(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return true;
        return false;
    }
}
