using UnityEditor;
using UnityEngine;

namespace xx.ulib
{
    /// <summary>
    /// GIF 创建菜单
    /// <para>author wx771720@outlook.com 2020-10-30 11:39:37</para>
    /// </summary>
    public class GIFInspector
    {
        public GIFInspector() { }

        [MenuItem("GameObject/xx/GIF", false, -1)]
        public static void CreateGIF()
        {
            GameObject parentGameObject = Selection.activeObject as GameObject;
            RectTransform parentTransform = parentGameObject?.GetComponent<RectTransform>();
            if (null == parentTransform) Debug.LogWarning("Your new GIF will not be visible until it is palced under a Canvas");
            GameObject gameObject = new GameObject("GIF", typeof(RectTransform), typeof(ComGIF));
            if (null != parentTransform) gameObject.transform.SetParent(parentTransform, false);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = gameObject;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}