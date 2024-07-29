#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Scripts.Editor
{
    public class EditorSwitchSceneFeature
    {
        [MenuItem("ToolBox/场景Scene/Cook", priority = 1)]
        public static void SwitchToCookScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Cook.unity");
        }

        [MenuItem("ToolBox/场景Scene/Sale", priority = 2)]
        public static void SwitchToSaleScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Sale.unity");
        }
    }
}

#endif