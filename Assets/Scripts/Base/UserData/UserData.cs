using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Game.CSV.Partial;
using UnityEngine.Video;

namespace Scripts.Base.UserData
{
    public class UserData
    {
        //TODO 声明玩家属性

        public UserData()
        {
            
        }

        public void InitData()
        {
            //TODO 处理初始化玩家数据

            UserDataManager.Instance.SaveData();
        }

    }
}