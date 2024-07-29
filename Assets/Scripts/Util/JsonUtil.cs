using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Scripts.Utils
{
    public class JsonUtil
    {
        
        public static T Load<T>(string nPath) where T : new()
        {
            string nJson = Path.Combine(Application.persistentDataPath, nPath);
            nJson = nJson.Replace("/", "\\");
            Debug.Log($"Json {nJson}");
            if (!File.Exists(nJson))
            {
                T nData = new T();
                Save(nData, nPath);
                return nData;
            }

            string nContent = File.ReadAllText(nJson);
            return JsonConvert.DeserializeObject<T>(nContent);
        }

        public static void Save<T>(T nObject, string nPath)
        {
            string nJson = Path.Combine(Application.persistentDataPath, nPath);
            nJson = nJson.Replace("/", "\\");
            string nDirectory = Path.GetDirectoryName(nJson);
            if (!Directory.Exists(nDirectory))
                Directory.CreateDirectory(nDirectory);
            var nContent = JsonConvert.SerializeObject(nObject);
            File.WriteAllText(nJson, nContent);
        }
    }
}