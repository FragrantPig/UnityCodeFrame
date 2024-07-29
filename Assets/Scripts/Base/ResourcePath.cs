using UnityEngine;
using System.IO;

public class ResourcePath
{
    private const string UI_PREFAB_PATH = "Prefab";
    private const string PANEL_NAME_FORMAT = "Panel/UI{0}Panel";
    private const string OVERLAY_NAME_FORMAT = "Overlay/UI{0}Overlay";
    private const string DIALOG_PREFAB_PATH = "Prefab/Dialog";
    private const string UIMANAGER_GAMEOBJECT_PATH = "Prefab/UIManager";


    public static string GetUIManagerPath()
    {
        return UIMANAGER_GAMEOBJECT_PATH;
    }

    public static string GetPanelPath(string nViewName)
    {
        var nName = string.Format(PANEL_NAME_FORMAT, nViewName);
        return Path.Combine(UI_PREFAB_PATH, nName);
    }

    public static string GetOverlayPath(string nOverlayName)
    {
        var nName = string.Format(OVERLAY_NAME_FORMAT, nOverlayName);
        return Path.Combine(UI_PREFAB_PATH, nName);
    }

    public static string GetDialogPath(string nDialogName)
    {
        var names = nDialogName.Split(".");
        var name = names[names.Length - 1];
        return Path.Combine(DIALOG_PREFAB_PATH, name);
    }
}