#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Scripts.Editor.CSV
{
    public class EditorBuildCsvClass
    {
        private const string PATH_CSV_ROOT = "_CSV";
        private const string PATH_CSHARP_ROOT = "Scripts/Game/CSV";
        private const string PATH_MASTERDATA = "_CSV/MasterData.csv";
        
        private const string FORMAT_CLASS_CONTENT = @"
//BUILT BY SCRIPT!!! DO NOT EDITOR!!!
namespace Scripts.Game.CSV
{{
    public partial class Master{0} : IDeepCopyable<Master{0}>
    {{
        {1}
        {2}
    }}
}}";

        private const string FORMAT_PROPERTY_CONTENT = @"
        public {0} {1} {{ get; set; }}";

        private const string FORMAT_METHOD_CONTENT = @"
        public Master{0} DeepCopy()
        {{
            Master{0} result = new Master{0}();{1}
            return result;
        }}";
        
        private const string FORMAT_METHOD_PARAMS_CONTENT = @"
            result.{0} = {0};";
        
        
        [MenuItem("ToolBox/Csv/WriteAllCsvClass")]
        public static void WriteAllCsvClass()
        {
            var nAllCsvFiles = GetCsvList();
            foreach (var val in nAllCsvFiles)
            {
                var nContent = GetCsvClass(val, true);
                WriteCSharpFile(GetCSharpFilePath(val._fileName), nContent, true);
            }
            Debug.Log($"构建全部CSV类完成...");
        }
        
        public static string GetCsvClass(CsvData nData, bool nForce = false)
        {
            string nClassContent = string.Empty;
            string nPropertyContent = string.Empty;
            string nMethodContent = string.Empty;
            string nMethodParamsContent = string.Empty;
            foreach (var mbr in nData._typeAndName)
            {
                nPropertyContent += GetClassFile(FORMAT_PROPERTY_CONTENT, mbr.Key, mbr.Value);
                nMethodParamsContent += string.Format(FORMAT_METHOD_PARAMS_CONTENT, mbr.Value);
            }
            nMethodContent = string.Format(FORMAT_METHOD_CONTENT, nData._fileName, nMethodParamsContent);
            nClassContent = string.Format(FORMAT_CLASS_CONTENT, nData._fileName, nPropertyContent, nMethodContent).Trim();
            return nClassContent;
        }

        public static void WriteCSharpFile(string nPath, string nContent, bool nForce = false)
        {
            string nCSharpDir = Path.GetDirectoryName(nPath);
            if (!Directory.Exists(nCSharpDir))
                Directory.CreateDirectory(nCSharpDir);
            else
            {
                if(nForce)
                    File.Delete(nPath);
            }
            // 文件写入
            File.WriteAllText(nPath, nContent);
            AssetDatabase.Refresh();
            Debug.Log($"构建CSV类：{nPath} 成功");
        }
        
        private static List<CsvData> GetCsvList()
        {
            string nMstDataPath = Path.Combine(Application.dataPath, PATH_MASTERDATA);
            List<CsvData> nAllMstData = new List<CsvData>();
            using (var fs = new StreamReader(nMstDataPath))
            {
                while (!fs.EndOfStream)
                {
                    var nLine = fs.ReadLine();
                    string[] nBlock = nLine.Split(',');
                    CsvData nValue = new CsvData();
                    nValue._fileName = nBlock[0];
                    foreach (var block in nBlock)
                    {
                        var nTypeValue = block.Split(' ');
                        if (nTypeValue.Count() != 2)
                            continue;
                        nValue._typeAndName.Add(new KeyValuePair<string, string>(nTypeValue[0], nTypeValue[1]));
                        Debug.Log($"filename = {nBlock[0]}, type = {nTypeValue[0]}, name = {nTypeValue[1]}");
                    }
                    nAllMstData.Add(nValue);
                }
            }
            return nAllMstData;
        }

        private static string GetClassFile(string nFormatProperty, string nType, string nName)
        {
            string nProperty = string.Empty;
            var nChars = nName.ToCharArray();
            nChars[0] = Char.ToUpper(nChars[0]);
            var nProName = new string(nChars);
            Debug.Log($"nType = {nType}, nName = {nName}, nProName = {nProName}");
            nProperty = String.Format(nFormatProperty, nType, nName, nProName);
            return nProperty;
        }

        private static string GetCSharpFilePath(string nFilePath)
        {
            var fileName = nFilePath;
            var filePath = nFilePath.Split("\\");
            if (filePath.Length > 0)
                if(filePath[0] == "Tea")
                    fileName = $"Tea\\Master{filePath[1]}";
            else
                fileName = $"Master{fileName}";
            string nPath = Path.Combine(Application.dataPath, PATH_CSHARP_ROOT, fileName);
            nPath = nPath.Replace("/", "\\");
            return Path.ChangeExtension(nPath, "cs");
        }
    }
}

public class CsvData
{
    public string _fileName;
    public List<KeyValuePair<string, string>> _typeAndName = new List<KeyValuePair<string, string>>();
}

#endif