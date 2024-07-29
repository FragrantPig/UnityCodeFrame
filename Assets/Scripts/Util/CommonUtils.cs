using System.Collections.Generic;

namespace Scripts.Utils
{
    public class CommonUtils
    {
        /// <summary>
        /// 移动游标，获取移动后对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nList"></param>
        /// <param name="nCurrent"></param>
        /// <param name="nMoveNum"></param>
        /// <returns></returns>
        public static T GetItem<T>(List<T> nList, T nCurrent, int nMoveNum) 
        {
            int nIndex = nList.IndexOf(nCurrent);
            int nAim = nIndex + nMoveNum;
            if (nAim <= 0)
                nAim = 0;
            else if (nAim >= nList.Count)
                nAim = nList.Count - 1;
            return nList[nAim];
        }
    }
}