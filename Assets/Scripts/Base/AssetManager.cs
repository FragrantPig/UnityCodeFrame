using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Base;
using Scripts.Game.UI.Base.Overlay;
using Scripts.UI;
using Scripts.UI.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Base
{

    public class AssetManager : MonoSingleton<AssetManager>, IMonoSingleton
    {
        public override bool IsDontDestroy => true;

        public T Load<T>(string nPath, Transform nParent)
        {
            Debug.Log($"加载资源Asset = {nPath}");
            var nPrefab = Resources.Load<GameObject>(nPath);
            var nGo = GameObject.Instantiate(nPrefab, nParent);
            nGo.transform.parent = nParent;
            return nGo.GetComponent<T>();
        }

        public void LoadDialog<T>(string nDialogName, Transform nParent = null, Action<T> nCallback = null) where T : DialogBase
        {
            StartCoroutine(LoadAsset<T>(ResourcePath.GetDialogPath(nDialogName), nParent, result =>
            {
                if (result.success)
                {
                    Debug.Log($"加载资源正常！Asset = {nDialogName}");
                    T nGo = (T)result.nGo;
                    nCallback(nGo);
                }
                else
                {
                    Debug.LogError("加载资源失败，Path= " + nDialogName);
                    nCallback(null);
                }
            }));
        }

        public void LoadPanel<T>(string nPanelName, Transform nParent = null, Action<T> nCallback = null) where T : BaseUIPanel
        {
            StartCoroutine(LoadAsset<T>(nPanelName, nParent, result =>
           {
               if (result.success)
               {
                   Debug.Log($"加载资源正常！Asset = {nPanelName}");
                   T nGo = (T)result.nGo;
                   nCallback(nGo);
               }
               else
               {
                   Debug.LogError("加载资源失败，Path= " + nPanelName);
                   nCallback(null);
               }
           }));
        }

        public void LoadOverlay<T>(string nOverlayName, Transform nParent = null, Action<T> nCallback = null) where T : BaseOverlay
        {
            StartCoroutine(LoadAsset<T>(nOverlayName, nParent, result =>
            {
                if (result.success)
                {
                    Debug.Log($"加载资源正常！Asset = {nOverlayName}");
                    T nOverlay = (T)result.nGo;
                    nOverlay.OnInitialize();
                    nCallback(nOverlay);
                }
                else
                {
                    Debug.LogError("加载资源失败，Path= " + nOverlayName);
                    nCallback(null);
                }
            }));
        }

        public void LoadUIManager(Transform nParent = null, Action<UIManager> nCallback = null)
        {
            StartCoroutine(LoadAsset<UIManager>(ResourcePath.GetUIManagerPath(), nParent, result =>
            {
                if (result.success)
                {
                    nCallback(result.nGo);
                }
                else
                {
                    nCallback(null);
                }
            }));
        }

        public void LoadSpriteAsync(string nSpriteName, Image nImg, System.Action<Sprite> nCallback = null)
        {
            LoadAssetAsync<Sprite>(nSpriteName, null, (sprite) =>
            {
                nImg.sprite = sprite as Sprite;
                nImg.SetNativeSize();
                nCallback?.Invoke(sprite);
            });
        }

        public void LoadAssetAsync<T>(string nAssetName, System.Action<T> nCallback = null) where T : UnityEngine.Object
        {
            LoadAssetAsync(nAssetName, null, nCallback);
        }

        public void LoadAssetAsync<T>(string nAssetName, Transform nParent = null, System.Action<T> nCallback = null) where T : UnityEngine.Object
        {
            StartCoroutine(LoadAsset<T>(nAssetName, nParent, result =>
            {
                if (result.success)
                {
                    Debug.Log($"加载资源正常！Asset = {nAssetName}");
                    T nGo = (T)result.nGo;
                    // 对 nGo 进行进一步操作
                    nCallback(nGo);
                }
                else
                {
                    Debug.LogError("加载资源失败，Path= " + nAssetName);
                    nCallback(null);
                }
            }));
        }

        public IEnumerator LoadAsset<T>(string nAssetName, Transform nParent = null, System.Action<AsyncResourceResult<T>> nCallback = null) where T : UnityEngine.Object
        {
            var nResourceRequest = Resources.LoadAsync<T>(nAssetName);
            nResourceRequest.completed += OnLoadSuccess;
            void OnLoadSuccess(AsyncOperation nResult)
            {
                try
                {
                    if (nResult.isDone)
                    {
                        T nGo = Instantiate(nResourceRequest.asset, nParent) as T;
                        nCallback(new AsyncResourceResult<T>(true, nGo));
                    }
                    else
                    {
                        nCallback(new AsyncResourceResult<T>(false, null));
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"【AssetManager】加载资源失败：{nAssetName}\n{e.Message}\n {e.StackTrace}");
                }
            }
            yield return null;
        }

        public class AsyncResourceResult<T> where T : UnityEngine.Object
        {
            public bool success;
            public T nGo;

            public AsyncResourceResult(bool success, T nGo)
            {
                this.success = success;
                this.nGo = nGo;
            }
        }
    }

}