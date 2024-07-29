using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Scripts.Base;
using Scripts.UI;
using Scripts.Game.UI.Base.Overlay;
using Cinemachine;
using Scripts.Base.Manager;

namespace Scripts.UI.Manager
{
    using ViewLoadFinish = UnityAction;

    public class UIManager : MonoBehaviour, IDisposable
    {
        private static UIManager _instance;

        private const int DIALOG_CAPACITY = 10;
        private SafeStack<DialogBase> _allDialogs = new SafeStack<DialogBase>(DIALOG_CAPACITY);

        [SerializeField] private Transform _root;
        [SerializeField] private Transform _overlay;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private CinemachineBrain _cameraBrain;
        [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;
        [SerializeField] public List<CinemachineVirtualCamera> _cameras = new List<CinemachineVirtualCamera>();

        private IViewController _currentViewController;
        private BaseUIPanel _currentPanel;
        private BaseOverlay _currentOverlay;

        public Transform Root
        {
            get
            {
                if (_root == null)
                    return null;
                return _root;
            }
        }

        public static UIManager Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public Transform GetOverlayRoot()
        {
            return _overlay;
        }

        #region Dialog

        public void PushDialog<TDialog>(Action<TDialog> nCallback = null) where TDialog : DialogBase
        {
            var nDialogName = typeof(TDialog).Name;
            AssetManager.Instance.LoadDialog<TDialog>(nDialogName, Root, (nDialog) =>
            {
                _allDialogs.Push(nDialog);
            });
            ;
        }

        public void PullDialog()
        {
            DialogBase nDialog;
            _allDialogs.TryPop(out nDialog);
            if (nDialog != null)
                Destroy(nDialog.gameObject);
        }

        #endregion

        public void LoadView(ViewID nViewId, ViewLoadFinish nCallback = null)
        {
            IViewData nData;
            if (ViewDefine.TryGetViewData(nViewId, out nData))
            {
                _currentViewController = nData.GenerateViewController();
            }
            else
                Debug.LogError($"找不到对应UIController ViewID = {nViewId.ToString()}");

            //加载Overlay
            var nOverlayName = nData.GetOverlayPath();
            Debug.Log($"UT: nOverlayName = {nOverlayName}");
            var nOverlayPath = nData.GetOverlayPath();
            if (!string.IsNullOrEmpty(nOverlayName))
            {
                AssetManager.Instance.LoadOverlay<BaseOverlay>(nOverlayPath, _overlay, (nGo) =>
                {
                    _currentOverlay = nGo as BaseOverlay;
                });
            }
            
            //加载Overlay
            var nPath = nData.GetViewPrefabPath();
            if (_currentPanel != null)
            {
                Debug.Log($"卸载UI预制件 _currentPanel = {_currentPanel.transform.name}");
                Destroy(_currentPanel.gameObject);
            }
            AssetManager.Instance.LoadPanel<BaseUIPanel>(nPath, _root, (nGo) =>
            {
                _currentPanel = nGo.GetComponent<BaseUIPanel>();
                _currentViewController.SetBaseUI(_currentPanel);
                InputManager.Instance.SetInputController(_currentViewController);
                StartCoroutine(_currentViewController.InitializeView());
                StartCoroutine(_currentViewController.UpdateView());
                nCallback?.Invoke();
            });
        }

        public IViewController GetViewController()
        {
            return _currentViewController;
        }

        public BaseOverlay GetOverlay()
        {
            return _currentOverlay;
        }

        public Vector3 ScreenToWorldPoint(Vector3 inputPos)
        {
            return _mainCamera.ScreenToWorldPoint(inputPos);
        }

        #region Camera

        public CinemachineVirtualCamera GetMainVirtualCamera()
        {
            return _mainVirtualCamera;
        }

        public void SetBrainToCut()
        {
            SetCameraBrainBlendType(CinemachineBlendDefinition.Style.Cut);
        }

        public void SetBrainToEaseInOut()
        {
            SetCameraBrainBlendType(CinemachineBlendDefinition.Style.EaseInOut);
        }

        public void SetCameraBrainBlendType(CinemachineBlendDefinition.Style nStyle)
        {
            _cameraBrain.m_DefaultBlend.m_Style = nStyle; 
        }

        public void ResetAllCameraPriority()
        {
            foreach (var val in _cameras)
                val.Priority = CameraDefine.Priority_Lower;
            UIManager.Instance.GetMainVirtualCamera().Priority = CameraDefine.Priority_Main;
        }

        public void SwitchCamera(int nCameraId)
        {
            ResetAllCameraPriority();
            if (nCameraId == 0)
                SetToMainCamera();
            else
                SetCameraPriority(_cameras[nCameraId], 11);
        }

        public void SetCameraPriority(CinemachineVirtualCamera nCamera, int nPriority)
        {
            nCamera.Priority = nPriority;
        }

        public void SetToMainCamera()
        {
            SetCameraPriority(_cameras[0], CameraDefine.Priority_Main);
        }

        #endregion Camera END

        public void Dispose()
        {
            _currentOverlay = null;
            DestroyImmediate(_currentOverlay);
            _currentViewController = null;
            DestroyImmediate(_currentPanel);
            _currentPanel = null;
            DestroyImmediate(gameObject);
        }
    }
}
