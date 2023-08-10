using UnityEditor;

namespace xx.ulib
{
    /// <summary>
    /// 资源管理
    /// <para>author wx771720@outlook.com 2020-10-29 11:22:00</para>
    /// </summary>
    public partial class AssetEditor : AssetPostprocessor
    {
        public AssetEditor() : base() { }

        public static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFromAssetPaths)
        {
            foreach (string path in imported)
            {
                if (path.EndsWith(".gif")) CreateGIFAsset(path);//创建 gif 资源
            }
        }
    }
}