#if UNITY_ENGINE
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

public class TestAssetDatabase
{
    [MenuItem("AssetDatabase/OutputLibraryPathsForAsset")]
    public static void OutputLibraryPathsForAsset()
    {
        var assetPath = "Assets/Resources/Prefab/Cook/food_L.prefab";

        StringBuilder assetPathInfo = new StringBuilder();

        var guidString = AssetDatabase.AssetPathToGUID(assetPath);
        //The ArtifactKey is needed here as there are plans to
        //allow importing for different platforms without switching
        //platform, thus ArtifactKeys will be parametrized in the future
        var artifactKey = new ArtifactKey(new GUID(guidString));
        var artifactID = AssetDatabaseExperimental.LookupArtifact(artifactKey);

       //Its possible for an Asset to have multiple import results,
       //if, for example, Sub-assets are present, so we need to iterate
        //over all the artifacts paths
        AssetDatabaseExperimental.GetArtifactPaths(artifactID, out var paths);

        assetPathInfo.Append($"Files associated with {assetPath}");
        assetPathInfo.AppendLine();

        foreach (var curVirtualPath in paths)
        {
            //The virtual path redirects somewhere, so we get the
            //actual path on disk (or on the in memory database, accordingly)
            var curPath = Path.GetFullPath(curVirtualPath);
            assetPathInfo.Append("\t" + curPath);
            assetPathInfo.AppendLine();
        }

        Debug.Log("Path info for asset:\n"+assetPathInfo.ToString());
    }
}
#endif