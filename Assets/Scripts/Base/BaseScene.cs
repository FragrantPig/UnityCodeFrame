using Scripts.Manager;
using Scripts.Scene;
using Scripts.UI;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    private static bool _isStarted = false;
    protected virtual ViewID MainViewId => _mainViewId;
    private ViewID _mainViewId = ViewID.None;
    protected virtual SceneID SceneID => _sceneId;
    private SceneID _sceneId = SceneID.GameStart;

    private void Start()
    {
        OnInitial();
    }

    protected virtual void OnInitial()
    {
        if (!_isStarted)
        {
            UTSceneManager.Instance.LoadScene(SceneID, _isStarted, OnLoadComplete);
            _isStarted = true;
        }
    }

    protected virtual void OnLoadComplete()
    {

    }
}