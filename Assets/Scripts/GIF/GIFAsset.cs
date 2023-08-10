using UnityEngine;

namespace xx.ulib
{
    /// <summary>
    /// GIF 自定义资源
    /// <param>author wx771720@outlook.com 2020-10-29 16:00:10</para>
    /// </summary>
    [CreateAssetMenu(fileName = "GIF", menuName = "xx/gif", order = 1)]
    public class GIFAsset : ScriptableObject
    {
        public GIFAsset() : base() { }

        [Tooltip("帧图集")]
        public Texture2D[] Textures;
        [Tooltip("宽度")]
        public int Width;
        [Tooltip("高度")]
        public int Height;
        [Tooltip("播放间隔（单位：毫秒）")]
        public int Interval;
    }
}