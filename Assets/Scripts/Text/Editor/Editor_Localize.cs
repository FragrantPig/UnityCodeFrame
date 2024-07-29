#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Scripts.Game.CSV;
using Scripts.Game.CSV.Manager;
using UnityEditor;
using UnityEngine;

namespace Scripts.Base.Text
{
    public class Editor_Localize
    {
        private const string PATH_LOCALIZE_CSV = "Text/Localize.csv";
        private const string PATH_LOCALIZE_CS_FILE = "Scripts/Text/LocalizeDictionary.cs";

        [MenuItem("ToolBox/Localize/生成Localize中文")]
        public static void GenerateLocalizeDictionary_CN()
        {
            GenerateLocalizeDictionary(Localize.Region.CN);
        }

        [MenuItem("ToolBox/Localize/生成Localize英文")]
        public static void GenerateLocalizeDictionary_EN()
        {
            GenerateLocalizeDictionary(Localize.Region.EN);
        }

        [MenuItem("ToolBox/Localize/生成Localize日文")]
        public static void GenerateLocalizeDictionary_JP()
        {
            GenerateLocalizeDictionary(Localize.Region.JP);
        }

        public static void GenerateLocalizeDictionary(Localize.Region nRegion)
        {
            if (File.Exists(PATH_LOCALIZE_CS_FILE))
                File.Delete(PATH_LOCALIZE_CS_FILE);
            var nAllLocalizes = MasterDataManager.ReadCsv<MasterLocalize>(PATH_LOCALIZE_CSV);
            string nContent = STATIC_VAR_DEF_LOCALIZE_TEMPLATE;
            StringBuilder nStringText = new StringBuilder();
            StringBuilder nStringEnum = new StringBuilder();
            for (int i = 0; i < nAllLocalizes.Count; i++)
            {
                nStringEnum.Append($"{nAllLocalizes[i].id},\n            ");
                var nLocalTxt = GetLocalizeByRegion(nRegion, nAllLocalizes[i]);
                nStringText.Append($"{{ TextID.{nAllLocalizes[i].id}, \"{nLocalTxt}\"}},\n            ");
            }
            nContent = nContent.Replace("$TEXTENUM_TEXT_STRINGS", nStringEnum.ToString());
            nContent = nContent.Replace("$TEXTID_TEXT_STRINGS", nStringText.ToString());

            var nFilePath = Path.Combine(Application.dataPath, PATH_LOCALIZE_CS_FILE);
            string? directoryPath = Path.GetDirectoryName(nFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllText(nFilePath, nContent, new System.Text.UTF8Encoding());
            AssetDatabase.Refresh();
        }

        private static string GetLocalizeByRegion(Localize.Region nRegion, MasterLocalize nData)
        {
            switch (nRegion)
            {
                case Localize.Region.CN:
                    return nData.cn;
                case Localize.Region.EN:
                    return nData.en;
                case Localize.Region.JP:
                    return nData.jp;
                default:
                    return nData.cn;
            }
        }

        private const string STATIC_VAR_DEF_LOCALIZE_TEMPLATE = @"// *WARNING* THIS IS TOOL GENERATED CODE. DO NOT EDIT!
using System.Collections.Generic;

namespace Scripts.Base.Text
{
    public partial class LocalizeDictionary
    {
        public enum TextID
        {
            $TEXTENUM_TEXT_STRINGS
        }

        public static Dictionary<TextID, string> TextDictionary = new Dictionary<TextID, string>()
        {
            $TEXTID_TEXT_STRINGS
        };
    }
}";
    }
}
#endif