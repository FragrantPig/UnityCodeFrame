using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Scripts.Base;
using JetBrains.Annotations;
using UnityEngine;

namespace Scripts.Game.CSV.Manager
{
    public partial class MasterDataManager : Singleton<MasterDataManager>, ISingleton
    {
        private const string PATH_CSV_ROOT = "_CSV";

        public const float WEIGHT_TO_INT_RATE = 0.01f;

        public List<MasterExampleData> MasterExampleData = new List<MasterExampleData>();
        public List<MasterText> MasterText = new List<MasterText>();

        public void LoadAllCsv()
        {
            foreach (var csv in AllCSV)
            {
                switch (csv)
                {
                    case "Example/ExampleData":
                        MasterExampleData = ReadCsv<MasterExampleData>("Example/ExampleData.csv");
                        break;
                    case "Text/Text":
                        MasterText = ReadCsv<MasterText>("Text/Text.csv");
                        break;
                }
            }
        }

        public static List<T> ReadCsv<T>(string nFilePath) where T : IDeepCopyable<T>
        {
            List<T> result = new List<T>();
            using (var sr = new StreamReader(Path.Combine(Application.dataPath, PATH_CSV_ROOT, nFilePath)))
            {
                using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    var temp = csv.GetRecords<T>();
                    foreach (var record in temp)
                    {
                        result.Add(record.DeepCopy());
                    }
                }
            }
            return result;
        }

        private HashSet<string> AllCSV = new HashSet<string>()
        {
            "Example/ExampleData",
            "Text/Text"
        };
    }
}