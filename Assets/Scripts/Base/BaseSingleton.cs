using System;
using UnityEngine;

namespace Scripts.Base
{
    public class Singleton<T> : BaseSingleton<T>
        where T : ISingleton, new()
    {
        public virtual void OnInitialize()
        {

        }
    }

    public class MonoSingleton<T> : BaseMonoSingleton<T>
        where T : MonoBehaviour, IMonoSingleton
    {
        public virtual void OnInitialize()
        {

        }
    }

    public class BaseSingleton<T> : IDisposable
        where T : ISingleton, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    _instance.OnInitialize();
                }
                return _instance;
            }
        }

        public virtual void Dispose()
        {
            _instance = default(T);
        }

    }

    public class BaseMonoSingleton<T> : MonoBehaviour
        where T : MonoBehaviour, IMonoSingleton
    {
        private static T _instance;
        public virtual bool IsDontDestroy => false;
        private const string MANAGER_ROOT_NAME = "ManagerPool";
        private static GameObject _root;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject temp = new GameObject(typeof(T).Name);
                    _instance = temp.AddComponent<T>();
#if UNITY_EDITOR
                    if (_instance.IsDontDestroy)
                    {
                        DontDestroyOnLoad(temp);
                    }
#endif                
                    _root = GameObject.Find(MANAGER_ROOT_NAME);
                    if (_root == null)
                    {
                        _root = new GameObject(MANAGER_ROOT_NAME);
                        DontDestroyOnLoad(_root);
                    }
                    _instance.transform.parent = _root.transform;
                    _instance.OnInitialize();
                }
                return _instance;
            }
        }
    }

    public interface IMonoSingleton
    {
        bool IsDontDestroy { get; }
        void OnInitialize();
    }

    public interface ISingleton
    {
        void OnInitialize();
    }
}