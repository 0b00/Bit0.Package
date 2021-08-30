namespace Bit0.Registry.Core.Extensions
{
    public static class StringExtentions
    {
        public static String Replace(this String str, Char newVal, params Char[] seperators)
        {
            return String.Join(newVal, str.Split(seperators));
        }
    }
}
