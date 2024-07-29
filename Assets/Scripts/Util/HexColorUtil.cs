using UnityEngine;

namespace Scripts.Utils
{
    public class HexColorUtil
    {
        public static Color HexToColor(string nHex, float nAlpha)
        {
            nAlpha = Mathf.Clamp(nAlpha, 0f, 1f);

            // 移除可能的前缀 #
            if (nHex.StartsWith("#"))
            {
                nHex = nHex.Substring(1);
            }

            // 解析十六进制颜色码
            byte r = byte.Parse(nHex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(nHex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(nHex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            // 创建并返回Color对象
            return new Color32(r, g, b, (byte)(255 * nAlpha));
        }

        public static Color HexToColor(string nHex)
        {
            return HexToColor(nHex, 1f);
        }
    }
}