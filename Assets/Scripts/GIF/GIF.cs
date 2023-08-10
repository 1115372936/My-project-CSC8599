using UnityEngine;
using xx.ulib;

namespace xx
{
    /// <summary>
    /// 
    /// <para>author wx771720@outlook.com 2020-10-29 18:29:47</para>
    /// </summary>
    public class GIF
    {
        public GIF() { }

        /// <summary>
        /// 颜色集
        /// </summary>
        public int[][] Colors;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width;
        /// <summary>
        /// 高度
        /// </summary>
        public int Height;
        /// <summary>
        /// 播放间隔（单位：毫秒）
        /// </summary>
        public int Interval;

        public Texture2D[] ToTextures() { return UtilUnity.ToTextures(Colors, Width, Height); }
    }
}