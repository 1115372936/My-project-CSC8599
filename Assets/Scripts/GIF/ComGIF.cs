using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xx.ulib
{
    /// <summary>
    /// GIF 组件
    /// </summary>
    [AddComponentMenu("xx/Com GIF")]
    public class ComGIF : RawImage
    {
        public ComGIF() : base() { }

        [Tooltip("GIF 资源")]
        public GIFAsset Asset;
        [Tooltip("循环播放次数，0 表示无限循环")]
        public uint Loop = 0;

        private float time = 0;
        private int index = 0;
        private int counted = 0;
        private bool isPaused = false;
        private Dictionary<int, List<Action>> frameHandlerMap = new Dictionary<int, List<Action>>();

        private bool canPlay { get { return null != Asset && null != Asset.Textures && Asset.Textures.Length > 0; } }

        public ComGIF SetAsset(GIFAsset asset, bool setNative = true)
        {
            Asset = asset;
            if (setNative) SetNativeSize();
            return this;
        }
        public override void SetNativeSize()
        {
            RectTransform transform = GetComponent<RectTransform>();

            transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, null == Asset ? 0 : Asset.Width);
            transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, null == Asset ? 0 : Asset.Height);
        }
        protected override void Start()
        {
            if (canPlay)
            {
                time = Time.time;
                texture = Asset.Textures[index];
            }
            else texture = null;
        }
        void Update()
        {
            if (canPlay)
            {
                if (!isPaused && (0 == Loop || counted < Loop) && (Time.time - time) * 1000 > Asset.Interval)
                {
                    time = Time.time;
                    texture = Asset.Textures[index++];
                    if (frameHandlerMap.ContainsKey(index - 1))
                    {
                        List<Action> handlers = frameHandlerMap[index].GetRange(0, frameHandlerMap[index].Count);
                        foreach (Action handler in handlers) { handler(); }
                    }
                    if (index >= Asset.Textures.Length)
                    {
                        index = 0;
                        counted++;
                    }
                }
            }
            else texture = null;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public ComGIF Pause() { isPaused = true; return this; }
        /// <summary>
        /// 继续
        /// </summary>
        public ComGIF Resume() { isPaused = false; return this; }

        /// <summary>
        /// 显示指定帧
        /// </summary>
        /// <param name="index">帧，-1 表示最后一帧</param>
        public ComGIF ShowFrame(int index)
        {
            if (canPlay)
            {
                index = ValidateIndex(index);
                texture = Asset.Textures[index];
            }
            return this;
        }
        public ComGIF AddFrameListener(Action handler, int index = -1)
        {
            if (canPlay)
            {
                index = ValidateIndex(index);
                if (!frameHandlerMap.ContainsKey(index)) frameHandlerMap[index] = new List<Action>() { handler };
                else
                {
                    List<Action> handlers = frameHandlerMap[index];
                    if (handlers.Contains(handler)) handlers.Remove(handler);
                    handlers.Add(handler);
                }
            }
            return this;
        }
        public ComGIF RemoveFrameListener(Action handler = null, int? index = null)
        {
            if (null == index) frameHandlerMap.Clear();
            else if (canPlay)
            {
                index = ValidateIndex(index.Value);
                if (frameHandlerMap.ContainsKey(index.Value))
                {
                    if (null == handler) frameHandlerMap.Remove(index.Value);
                    else
                    {
                        List<Action> handlers = frameHandlerMap[index.Value];
                        if (handlers.Contains(handler)) handlers.Remove(handler);
                    }
                }
            }
            return this;
        }

        private int ValidateIndex(int index)
        {
            if (index < 0) index += Asset.Textures.Length;
            if (index < 0) index = 0;
            if (index >= Asset.Textures.Length) index = Asset.Textures.Length - 1;
            return index;
        }
    }
}