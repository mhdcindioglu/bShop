namespace bShop.Data.Helpers;

public static class Helper
{
    public static string Generate10DigitNumber()
    {
        Random random = new();
        string result = "";

        for (int i = 0; i < 10; i++)
            result = $"{result}{random.Next(0, 10)}";

        return result;
    }
}
