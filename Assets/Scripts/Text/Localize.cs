using static Scripts.Base.Text.LocalizeDictionary;

namespace Scripts.Base.Text
{
    public class Localize
    {
        public enum Region
        {
            CN = 0,
            EN = 1,
            JP = 2,
        }

        public static string Get(TextID nID, params object[] nParams)
        {
            var nFormat = Get(nID);
            return string.Format(nFormat, nParams);
        }
        
        public static string Get(TextID nID)
        {
            string nText = string.Empty;
            if(!LocalizeDictionary.TextDictionary.TryGetValue(nID, out nText))
            {
                return null;
            }
            return nText;
        }
    }


}