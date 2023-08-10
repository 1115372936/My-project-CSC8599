using System.Collections.Generic;
using System.IO;

namespace xx
{
    /// <summary>
    /// GIF 文件解码器
    /// <para>author wx771720@outlook.com 2020-10-28 10:05:08</para>
    /// </summary>
    public static class GIFDecoder
    {
        /// <summary>
        /// 字节流
        /// </summary>
        private static MemoryStream stream;
        //-----------------------------------------------------------------------------
        //全局数据
        //-----------------------------------------------------------------------------
        /// <summary>
        /// 版本号
        /// </summary>
        private static string Version;
        /// <summary>
        /// 宽度
        /// </summary>
        public static int Width;
        /// <summary>
        /// 高度
        /// </summary>
        public static int Height;
        /// <summary>
        /// 播放间隔（单位：毫秒）
        /// </summary>
        public static int Interval;
        /// <summary>
        /// 全局颜色列表
        /// </summary>
        private static int[] GlobalColorTable;
        /// <summary>
        /// 全局背景颜色
        /// </summary>
        private static int GlobalBackgroundColor;
        //-----------------------------------------------------------------------------
        //图形控制扩展块
        //-----------------------------------------------------------------------------
        /// <summary>
        /// 是否使用透明颜色
        /// </summary>
        private static bool FrameTransparency;
        /// <summary>
        /// 暂停指定毫秒后继续
        /// </summary>
        private static int FrameDelay;
        /// <summary>
        /// 透明色索引
        /// </summary>
        private static int FrameTransparencyColorIndex;
        //-----------------------------------------------------------------------------
        //图像数据
        //-----------------------------------------------------------------------------
        /// <summary>
        /// 左边距
        /// </summary>
        private static int FrameOffsetLeft { get; set; }
        /// <summary>
        /// 上边距
        /// </summary>
        private static int FrameOffsetTop { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        private static int FrameFrameWidth { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        private static int FrameFrameHeight { get; set; }
        /// <summary>
        /// 是否交错排列
        /// </summary>
        private static bool FrameInterlace { get; set; }
        /// <summary>
        /// 颜色列表
        /// </summary>
        private static int[] FrameColorTable { get; set; }
        private static List<int[]> ColorsList = new List<int[]>();

        public static GIF Decode(byte[] bytes)
        {
            using (stream = new MemoryStream(bytes))
            {
                ReadHeader();
                ReadContents();
            }
            GIF gif = new GIF();
            gif.Colors = ColorsList.ToArray();
            gif.Width = Width;
            gif.Height = Height;
            gif.Interval = Interval;
            ColorsList.Clear();
            return gif;
        }

        private static char[] Signatures = new char[] { 'G', 'I', 'F' };
        private static bool ReadHeader()
        {
            //签名
            for (int index = 0; index < Signatures.Length; index++) { if (Signatures[index] != stream.ReadByte()) return false; }
            //版本号
            Version = new string(new char[] { (char)stream.ReadByte(), (char)stream.ReadByte(), (char)stream.ReadByte() });
            //尺寸
            Width = stream.ReadByte() | (stream.ReadByte() << 8);
            Height = stream.ReadByte() | (stream.ReadByte() << 8);
            //颜色
            int packed = stream.ReadByte();
            bool flag = 0 != (packed & 0x80);//是否有全局颜色列表
            int size = 2 << (packed & 7);//全局颜色列表大小
            int backgroundColorIndex = stream.ReadByte();//全局背景色索引
            stream.ReadByte();//像素长宽比，没啥用
            if (flag)
            {
                GlobalColorTable = ReadColorTable(size);
                GlobalBackgroundColor = GlobalColorTable[backgroundColorIndex];
            }
            return true;
        }
        private static bool ReadContents()
        {
            int flag;
            do
            {
                flag = stream.ReadByte();
                switch (flag)
                {
                    case 0x2C: ReadImage(); break; //图像开始标识
                    case 0x21: ReadExtensions(); break;//扩展块标识
                    case 0x3B: break;//块终结标识，直接忽略
                    case 0x00: break;//未知标识，直接忽略
                    default: return false;
                }
            } while (stream.CanRead);
            return true;
        }

        private static void ReadExtensions()
        {
            switch (stream.ReadByte())
            {
                case 0xF9: ReadGraphicControlExtension(); break;//图形控制扩展块
                case 0xFE: Skip(); break;//图形注释扩展块
                case 0x01: Skip(); break;//图形文本扩展块
                case 0xFF: Skip(); break;//应用程序扩展块
            }
        }
        private static void ReadGraphicControlExtension()
        {
            stream.ReadByte();//块大小，直接忽略
            int packed = stream.ReadByte();
            FrameTransparency = 0 != (packed & 1);
            FrameDelay = (stream.ReadByte() | (stream.ReadByte() << 8)) * 10;
            if (FrameDelay > Interval) Interval = FrameDelay;
            FrameTransparencyColorIndex = stream.ReadByte();
            stream.ReadByte();//块终结标识
        }

        private static void ReadImage()
        {
            FrameOffsetLeft = stream.ReadByte() | (stream.ReadByte() << 8);
            FrameOffsetTop = stream.ReadByte() | (stream.ReadByte() << 8);
            FrameFrameWidth = stream.ReadByte() | (stream.ReadByte() << 8);
            FrameFrameHeight = stream.ReadByte() | (stream.ReadByte() << 8);
            //颜色
            int packed = stream.ReadByte();
            FrameInterlace = 0 != (packed & 0x40);
            FrameColorTable = 0 != (packed & 0x80) ? ReadColorTable(2 << (packed & 7)) : GlobalColorTable;
            //透明缓存
            int colorCache = 0;
            if (FrameTransparency)
            {
                colorCache = FrameColorTable[FrameTransparencyColorIndex];
                FrameColorTable[FrameTransparencyColorIndex] = 0;
            }
            //读取图件数据
            ReadImageData();
            Skip();
            //构造纹理
            SetPixels();
            //透明还原
            if (FrameTransparency) FrameColorTable[FrameTransparencyColorIndex] = colorCache;
        }
        private static readonly int MaxStackSize = 4096;
        private static byte[] pixels;
        private static short[] prefix;
        private static byte[] suffix;
        private static byte[] pixelStack;
        private static void ReadImageData()
        {
            int NullCode = -1;
            int npix = FrameFrameWidth * FrameFrameHeight;
            int available, clear, code_mask, code_size, end_of_information, in_code, old_code, bits, code, count, i, datum, data_size, first, top, bi, pi;

            if ((pixels == null) || (pixels.Length < npix)) pixels = new byte[npix];
            if (prefix == null) prefix = new short[MaxStackSize];
            if (suffix == null) suffix = new byte[MaxStackSize];
            if (pixelStack == null) pixelStack = new byte[MaxStackSize + 1];

            data_size = stream.ReadByte();
            clear = 1 << data_size;
            end_of_information = clear + 1;
            available = clear + 2;
            old_code = NullCode;
            code_size = data_size + 1;
            code_mask = (1 << code_size) - 1;
            for (code = 0; code < clear; code++)
            {
                prefix[code] = 0;
                suffix[code] = (byte)code;
            }

            datum = bits = count = first = top = pi = bi = 0;
            for (i = 0; i < npix;)
            {
                if (top == 0)
                {
                    if (bits < code_size)
                    {
                        if (count == 0)
                        {
                            count = ReadBlock(blockBytes);
                            if (count <= 0) break;
                            bi = 0;
                        }
                        datum += (((int)blockBytes[bi]) & 0xff) << bits;
                        bits += 8;
                        bi++;
                        count--;
                        continue;
                    }
                    code = datum & code_mask;
                    datum >>= code_size;
                    bits -= code_size;
                    if ((code > available) || (code == end_of_information)) break;
                    if (code == clear)
                    {
                        code_size = data_size + 1;
                        code_mask = (1 << code_size) - 1;
                        available = clear + 2;
                        old_code = NullCode;
                        continue;
                    }
                    if (old_code == NullCode)
                    {
                        pixelStack[top++] = suffix[code];
                        old_code = code;
                        first = code;
                        continue;
                    }
                    in_code = code;
                    if (code == available)
                    {
                        pixelStack[top++] = (byte)first;
                        code = old_code;
                    }
                    while (code > clear)
                    {
                        pixelStack[top++] = suffix[code];
                        code = prefix[code];
                    }
                    first = ((int)suffix[code]) & 0xff;
                    if (available >= MaxStackSize) break;
                    pixelStack[top++] = (byte)first;
                    prefix[available] = (short)old_code;
                    suffix[available] = (byte)first;
                    available++;
                    if (((available & code_mask) == 0) && (available < MaxStackSize))
                    {
                        code_size++;
                        code_mask += available;
                    }
                    old_code = in_code;
                }
                top--;
                pixels[pi++] = pixelStack[top];
                i++;
            }
            for (i = pi; i < npix; i++) { pixels[i] = 0; }
        }
        private static void SetPixels()
        {
            int[] colors = new int[Width * Height];
            int pass = 1;
            int inc = 8;
            int iline = 0;
            for (int i = 0; i < FrameFrameHeight; i++)
            {
                int line = i;
                if (FrameInterlace)
                {
                    if (iline >= FrameFrameHeight)
                    {
                        pass++;
                        switch (pass)
                        {
                            case 2: iline = 4; break;
                            case 3: iline = 2; inc = 4; break;
                            case 4: iline = 1; inc = 2; break;
                        }
                    }
                    line = iline;
                    iline += inc;
                }
                line += FrameOffsetTop;
                if (line < Height)
                {
                    int k = (Height - line - 1) * Width;
                    int dx = k + FrameOffsetLeft;
                    int dlim = dx + FrameFrameWidth;
                    if ((k + FrameFrameWidth) < dlim) dlim = k + Width;
                    int sx = i * FrameFrameWidth;
                    while (dx < dlim)
                    {
                        int index = ((int)pixels[sx++]) & 0xff;
                        colors[dx++] = FrameColorTable != null ? FrameColorTable[index] : 0;
                    }
                }
            }
            ColorsList.Add(colors);
        }



        private static byte[] blockBytes;
        private static void Skip()
        {
            if (null == blockBytes) blockBytes = new byte[255];
            while (ReadBlock(blockBytes) > 0) { }
        }
        private static int ReadBlock(byte[] bytes)
        {
            int size = stream.ReadByte();
            return 0 == size ? 0 : stream.Read(bytes, 0, size);
        }
        private static int[] ReadColorTable(int size)
        {
            int index = 0;
            int[] colorTable = new int[size];
            while (index < size)
            {
                colorTable[index++] = (0xFF << 24) | (stream.ReadByte() << 16) | (stream.ReadByte() << 8) | (stream.ReadByte());
            }
            return colorTable;
        }
    }
}