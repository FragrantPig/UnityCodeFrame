using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Base
{
    public class CachePool : MonoSingleton<CachePool>, IDisposable, IMonoSingleton
    {
        public override bool IsDontDestroy => true;
        [SerializeField]
        private static Dictionary<string, List<GameObject>> mCachePool = new Dictionary<string, List<GameObject>>();

        public void Dispose()
        {

        }

        public T Get<T>(string nKey, GameObject nOriginal, Transform nParent)
            where T : MonoBehaviour
        {
            return Get(nKey, nOriginal, nParent).GetComponent<T>();
        }

        public GameObject Get(string nKey, GameObject nOriginal, Transform nParent)
        {
            if (!mCachePool.ContainsKey(nKey))
                mCachePool.Add(nKey, new List<GameObject>());
            List<GameObject> nGoList = mCachePool[nKey];
            if (nGoList.Count <= 0)
            {
                var go = GameObject.Instantiate(nOriginal, nParent);
                return go;
            }
            var result = nGoList[0];
            result.transform.parent = nParent;
            nGoList.Remove(result);
            return result;
        }

        public void Return(string nKey, GameObject nGo)
        {
            if (!mCachePool.ContainsKey(nKey))
                mCachePool.Add(nKey, new List<GameObject>());
            List<GameObject> nGoList = mCachePool[nKey];
            nGoList.Add(nGo);
            nGo.gameObject.SetActive(false);
            nGo.transform.parent = this.transform;
        }
    }
}
