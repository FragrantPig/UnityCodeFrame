
using Scripts.Game.CSV.Partial;

public class TextUtil
{
    public static string Get(ETextCategory category, int id)
    {
        var data = MasterText.GetText((int)category, id);
        return data.text;
    }
}

public enum ETextCategory
{
    food_material = 101,
    recipe = 102,
}

public class Localize
{
    public const string Cropland_01 = "总计余额为：{0}";
}