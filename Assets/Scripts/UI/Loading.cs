using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private Image _img;

        private const float MINI_LOADING_TIME = 0.5f;
        private static Loading _instance;
        private static bool _loaded = false;
        private bool _isLoadOverMinTime = false;   // 控制loading页面停留的最低时长
        private bool _isLoadSuccess = false;    // 是否加载完成

        public static Loading Instance
        {
            get
            {
                if (_instance == null)
                {
                    var loadingPrefab = Resources.Load<GameObject>("Prefab/Loading");
                    _instance = Instantiate(loadingPrefab).GetComponent<Loading>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        private void Start()
        {
            _img.gameObject.SetActive(false);
        }

        public void SetAsLastSibling()
        {
            transform.SetAsLastSibling();
        }

        public void OnEnterLoading()
        {
            Debug.Log($"UT: Loading 开启");
            _img.gameObject.SetActive(true);
            _isLoadOverMinTime = false;
            _isLoadSuccess = false;
            StartCoroutine(LoadingLimitTime());
        }

        public void OnExitLoading()
        {
            Debug.Log($"UT: Loading 结束");
            _isLoadSuccess = true;
            IsLoadSuccess();
        }

        private IEnumerator LoadingLimitTime()
        {
            yield return new WaitForSeconds(MINI_LOADING_TIME);
            _isLoadOverMinTime = true;
            IsLoadSuccess();
        }

        private void IsLoadSuccess()
        {
            if (_isLoadOverMinTime && _isLoadSuccess)
                _img.gameObject.SetActive(false);
        }
    }
}