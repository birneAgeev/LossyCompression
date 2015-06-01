using System;

namespace WindowsFormsTemp.Jpeg
{
    [Serializable]
    public class JpegCoderSettings
    {
        public ThinningMode ThinningMode { get; set; }

        public int BlocSize
        {
            get { return 8; }
        }
    }
}