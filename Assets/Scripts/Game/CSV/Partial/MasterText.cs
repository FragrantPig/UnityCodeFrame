using Scripts.Game.CSV.Manager;

namespace Scripts.Game.CSV.Partial
{
    public partial class MasterText
    {
        public static CSV.MasterText GetText(int category, int id)
        {
            if (MasterDataManager.Instance == null || MasterDataManager.Instance.MasterText == null)
                return null;
            foreach (var optionTextData in MasterDataManager.Instance.MasterText)
            {
                if (optionTextData.category == category && optionTextData.id == id)
                {
                    return optionTextData;
                }
            }
            return null;
        }
    }
}