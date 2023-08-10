using System.IO;
using UnityEditor;
using UnityEngine;

namespace xx.ulib
{
    /// <summary>
    /// GIF 资源
    /// <para>author wx771720@outlook.com 2020-12-21 14:45:59</para>
    /// </summary>
    public partial class AssetEditor : AssetPostprocessor
    {
        /// <summary>
        /// 创建 gif 资源
        /// </summary>
        private static void CreateGIFAsset(string path)
        {
            if (!path.EndsWith(".gif")) return;
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                string folder = Path.GetDirectoryName(path);
                string name = Path.GetFileNameWithoutExtension(path);

                byte[] bytes = new byte[stream.Length];
                int count = stream.Read(bytes, 0, bytes.Length);

                GIFAsset asset = ScriptableObject.CreateInstance<GIFAsset>();
                xx.GIF gif = xx.GIFDecoder.Decode(bytes);
                asset.name = name;
                asset.Width = gif.Width;
                asset.Height = gif.Height;
                asset.Interval = gif.Interval;
                asset.Textures = gif.ToTextures();
                AssetDatabase.CreateAsset(asset, Path.Combine(folder, $"{name}.asset"));
                for (int index = 0; index < asset.Textures.Length; index++)
                {
                    AssetDatabase.AddObjectToAsset(asset.Textures[index], asset);
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}