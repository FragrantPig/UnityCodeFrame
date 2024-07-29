using Scripts.UI;
using UnityEngine;

namespace Scripts.Base
{
    public class AssetLoader
    {
        public static T LoadDialog<T>(string nDialogName, Transform nParent = null) where T : DialogBase
        {
            var nGo = LoadAsset<T>(ResourcePath.GetDialogPath(nDialogName), nParent);
            return nGo;
        }

        public static T LoadPanel<T>(string nPanelName, Transform nParent = null) where T : IViewable
        {
            var nGo = LoadAsset<GameObject>(nPanelName, nParent);
            return nGo.GetComponent<T>();
        }

        public static T LoadAsset<T>(string nAssetName, Transform nParent = null) where T : UnityEngine.Object
        {
            Debug.Log($"[UT] Load Asset: name = {nAssetName}");
            var nAsset = Resources.Load<T>(nAssetName);
            var nResult = GameObject.Instantiate<T>(nAsset, nParent);
            return nResult;
        }

    }
}