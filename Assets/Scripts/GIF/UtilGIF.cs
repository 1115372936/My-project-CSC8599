using UnityEngine;

namespace xx.ulib
{
    /// <summary>
    /// GIF 工具
    /// <param>author wx771720@outlook.com 2020-10-23 11:36:26</para>
    /// </summary>
    public partial class UtilUnity
    {
        public static Texture2D[] ToTextures(int[][] colorsList, int width, int height)
        {
            Texture2D[] textures = new Texture2D[colorsList.Length];
            int color;
            int[] colors;
            Texture2D texture;
            Color32[] uColors = null;
            for (int index = 0; index < colorsList.Length; index++)
            {
                colors = colorsList[index];
                if (null == uColors || uColors.Length != colors.Length) uColors = new Color32[colors.Length];
                for (int cIndex = 0; cIndex < uColors.Length; cIndex++)
                {
                    color = colors[cIndex];
                    uColors[cIndex].a = (byte)(color >> 24);
                    uColors[cIndex].r = (byte)(color >> 16);
                    uColors[cIndex].g = (byte)(color >> 8);
                    uColors[cIndex].b = (byte)color;
                }
                texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
                texture.SetPixels32(uColors);
                texture.Apply(false, false);
                textures[index] = texture;
            }
            return textures;
        }
    }
}