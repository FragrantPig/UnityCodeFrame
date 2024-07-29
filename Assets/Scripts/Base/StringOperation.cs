using UnityEngine;

public abstract class StringOperation
{
    private const string PrefixShakeLabel = "<shake>";
    private const string SuffixShakeLabel = "</shake>";
    
    public static bool ContainsShakeOrNot(string nContent)
    {
        if (!nContent.Contains(PrefixShakeLabel) && !nContent.Contains(SuffixShakeLabel))
            return false;
        if (nContent.Contains(PrefixShakeLabel) && nContent.Contains(SuffixShakeLabel))
            return true;
        else
        {
            Debug.LogError($"shake标签存在不匹配情况，错误文本：{nContent}");
            return false;
        }
    }
}