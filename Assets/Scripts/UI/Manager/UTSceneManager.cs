
using System.Collections;
using Scripts.Base;
using Scripts.Scene;
using Scripts.UI;
using Scripts.UI.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Game.CSV.Manager;
using Scripts.Base.UserData;

namespace Scripts.Manager
{
    using LoadSceneFinish = System.Action;
    using UnloadSceneFinish = System.Action;

    public class UTSceneManager : MonoSingleton<UTSceneManager>, IMonoSingleton
    {
        private BaseScene _currentScene;

        //目前只用来记录，当前场景
        private SceneID _sceneID;
        public SceneID Scene => _sceneID;
        private ViewID _viewId;
        private LoadSceneFinish OnLoadComplete = null;

        public void LoadScene(SceneID nSceneId)
        {
            StartCoroutine(_LoadScene(nSceneId, true, null));
        }

        public void LoadScene(SceneID nSceneId, bool nIsStartScene = true)
        {
            StartCoroutine(_LoadScene(nSceneId, nIsStartScene, null));
        }

        public void LoadScene(SceneID nSceneId, LoadSceneFinish nCallback = null)
        {
            StartCoroutine(_LoadScene(nSceneId, true, nCallback));
        }

        public void LoadScene(SceneID nSceneId, bool nIsStartScene, LoadSceneFinish nCallback)
        {
            StartCoroutine(_LoadScene(nSceneId, nIsStartScene, nCallback));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nSceneId"></param>
        /// <param name="nUnloadScene"> 是否已经启动</param>
        /// <param name="nCallback"></param>/
        public IEnumerator _LoadScene(SceneID nSceneId, bool nUnloadScene, LoadSceneFinish nCallback)
        {
            _sceneID = nSceneId;
            ISceneData nData = null;
            Loading.Instance.OnEnterLoading();
            if (SceneDefine.TryGetViewData(nSceneId, out nData))
            {
                if (!nUnloadScene)
                {
                    bool nIsUnloadScene = false;
                    StartCoroutine(_UnloadScene(() => nIsUnloadScene = true));
                    yield return new WaitUntil(() => nIsUnloadScene);
                }
                OnLoadComplete = nCallback;

                var nSceneName = nData.GetSceneName();
                var nViewId = nData.GetViewID();
                Debug.Log($"开始加载场景：{nSceneName}");
                SceneManager.LoadSceneAsync(nSceneName).completed += (d) =>
                {
                    if (d.isDone)
                    {
                        Debug.Log($"[UTSceneManager] 场景: {nSceneName} 加载完成。");
                        _currentScene = GameObject.Find($"{nSceneName}").GetComponent<BaseScene>();
                        _viewId = nViewId;
                        MasterDataManager.Instance.LoadAllCsv();
                        UserDataManager.Instance.ReadData();
                        AssetManager.Instance.LoadUIManager(null, (nGo) => { OnUIManagerLoadFinish(nGo); });
                    }
                };
            }
        }

        private void OnUIManagerLoadFinish(UIManager nGo)
        {
            Debug.Log($"[UTSceneManager] UIManager 加载成功。");
            UIManager.Instance = nGo;
            UIManager.Instance.LoadView(_viewId, OnViewLoadFinish);
        }

        private void OnViewLoadFinish()
        {
            Debug.Log($"[UTSceneManager] View: {_viewId} 加载完成。");
            OnLoadComplete?.Invoke();
            Loading.Instance.OnExitLoading();
            Debug.Log($"[UTSceneManager] 加载场景完毕。");
        }

        private IEnumerator _UnloadScene(UnloadSceneFinish nCallback)
        {
            yield return new WaitForEndOfFrame();
            if (_currentScene != null)
            {
                Debug.Log($"卸载场景：{_currentScene}");
                _currentScene = null;
                _viewId = ViewID.None;
                OnLoadComplete = null;
            }
            if (UIManager.Instance != null)
                UIManager.Instance.Dispose();
            if (MasterDataManager.Instance != null)
                MasterDataManager.Instance.Dispose();
            nCallback?.Invoke();
        }
    }
}