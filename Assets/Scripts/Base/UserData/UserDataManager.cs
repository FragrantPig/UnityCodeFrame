using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Scripts.Base;
using Scripts.Utils;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Scripts.Base.UserData
{
    public class UserDataManager : Singleton<UserDataManager>, ISingleton
    {
        private const string PATH_ARCHIVES = "..\\Archives";
#if UNITY_EDITOR
        private const string ARCHIVE_FILE_NAME = "Data\\UserData.json";
#else
        private const string ARCHIVE_FILE_NAME = "Data.dat";
#endif
        public const string PATH_CROP_DATA = "Data\\Crop_Record.json";

        private UserData _userData;
        public UserData Data => _userData;

        public override void OnInitialize()
        {
            ReadData();
            base.OnInitialize();
        }

        private static string GetArchiveFilePath()
        {
#if UNITY_EDITOR
            return ARCHIVE_FILE_NAME;
#else
            return Path.Combine(Application.dataPath, PATH_ARCHIVES, ARCHIVE_FILE_NAME);
#endif
        }

        private static string GetArchiveDirectoryPath()
        {
            return Path.Combine(Application.dataPath, PATH_ARCHIVES);
        }

        public void ReadData()
        {
#if UNITY_EDITOR
            _userData = JsonUtil.Load<UserData>(GetArchiveFilePath());
#else
             // 创建文件流并将对象写入文件
            using (FileStream stream = new FileStream(GetArchiveFilePath(), FileMode.Open))
            {
                // 使用BinaryFormatter将对象序列化为二进制数据
                BinaryFormatter formatter = new BinaryFormatter();
                _userData = formatter.Deserialize(stream) as UserData;
            }
#endif
            if (_userData == null)
            {
                Debug.Log($"不存在玩家数据文件，重新生成数据");
                _userData = new UserData();
                SaveData();
            }
            //TODO 若玩家数据不存在时，则为他生成数据

        }


        public void SaveData()
        {
#if UNITY_EDITOR
            Debug.Log($"保存数据中...");
            JsonUtil.Save(_userData, GetArchiveFilePath());
#else 
            SerializeObjectToBinaryFile(_userData);
#endif
        }

        private static void SerializeObjectToBinaryFile(UserData nData)
        {
            // 使用BinaryFormatter将对象序列化为二进制数据
            BinaryFormatter formatter = new BinaryFormatter();
            string nArchivePath = GetArchiveFilePath();
            var nDirectoryPath = Path.Combine(Application.dataPath, PATH_ARCHIVES);

            if (!Directory.Exists(nDirectoryPath))
                Directory.CreateDirectory(nDirectoryPath);

            // 创建文件流并将对象写入文件
            using (FileStream stream = new FileStream(nArchivePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, nData);
            }
        }

#if UNITY_EDITOR
        private const string USER_ARCHIVE_DIRECTORY = "Data";

        [MenuItem("ToolBox/UserData/玩家数据路径")]
        public static void OpenPersistentDataFolderWin()
        {
            Application.OpenURL(System.IO.Path.Combine(Application.persistentDataPath, USER_ARCHIVE_DIRECTORY));
        }

        [MenuItem("ToolBox/UserData/删除玩家数据")]
        public static void DeleteUserArchives()
        {
            Directory.Delete(System.IO.Path.Combine(Application.persistentDataPath, USER_ARCHIVE_DIRECTORY), true);
        }
#endif
    }
}