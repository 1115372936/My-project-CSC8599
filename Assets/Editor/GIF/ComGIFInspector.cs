using UnityEditor;
using UnityEngine;

namespace xx.ulib
{
    /// <summary>
    /// GIF 组件编辑面板
    /// <para>author wx771720@outlook.com 2020-10-29 16:07:44</para>
    /// </summary>
    [CanEditMultipleObjects, CustomEditor(typeof(ComGIF))]
    public class ComGIFInspector : Editor
    {
        public ComGIFInspector() : base() { }

        private SerializedProperty _asset;
        private SerializedProperty _loop;
        private SerializedObject _transform;
        private SerializedProperty _sizeDelta;

        private void OnEnable()
        {
            _asset = serializedObject.FindProperty("Asset");
            _loop = serializedObject.FindProperty("Loop");
            _transform = new SerializedObject((target as ComGIF).gameObject.transform);
            _sizeDelta = _transform.FindProperty("m_SizeDelta");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_asset);
            EditorGUILayout.PropertyField(_loop);
            if (GUILayout.Button("Set Native Size", GUILayout.ExpandWidth(true)))
            {
                GIFAsset asset = _asset.objectReferenceValue as GIFAsset;
                if (null != asset)
                {
                    _sizeDelta.vector2Value = new Vector2(asset.Width, asset.Height);
                    _transform.ApplyModifiedProperties();
                }
            }
            if (GUI.changed) serializedObject.ApplyModifiedProperties();
        }
    }
}