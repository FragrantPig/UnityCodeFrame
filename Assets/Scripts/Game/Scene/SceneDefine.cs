using System.Collections.Generic;
using Scripts.UI;

namespace Scripts.Scene
{
    public enum SceneID
    {
        GameStart = 1,
        Example = 2,
    }

    public interface ISceneData
    {
        public string GetSceneName();
        public ViewID GetViewID();
    }

    public class SceneData : ISceneData
    {
        public SceneID _sceneId;
        public ViewID _viewId;

        public SceneData(SceneID nSceneId, ViewID nViewId)
        {
            _sceneId = nSceneId;
            _viewId = nViewId;
        }

        public string GetSceneName()
        {
            return _sceneId.ToString();
        }

        public ViewID GetViewID()
        {
            return _viewId;
        }
    }

    public class SceneDefine
    {
        public static readonly Dictionary<SceneID, ISceneData> SCENE_DIC = new Dictionary<SceneID, ISceneData>()
        {
            { SceneID.Example, new SceneData(SceneID.Example, ViewID.Example)},
        };
        public static bool TryGetViewData(SceneID nSceneId, out ISceneData nData)
        {
            if (SCENE_DIC.TryGetValue(nSceneId, out nData))
            {
                return true;
            }

            return false;
        }
    }
}
