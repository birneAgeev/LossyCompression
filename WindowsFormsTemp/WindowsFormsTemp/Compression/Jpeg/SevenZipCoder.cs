using System;
using System.IO;
using SevenZip.SDK.Compress.LZMA;

namespace WindowsFormsTemp.Compression.Jpeg
{
    public class SevenZipCoder
    {
        public static SevenZipCoder Instance = new SevenZipCoder();

        private SevenZipCoder()
        {
        }

        public byte[] Encode(byte[] data)
        {
            var inputStream = new MemoryStream(data);
            var outputStream = new MemoryStream();

            var coder = new Encoder();
            coder.WriteCoderProperties(outputStream);
            outputStream.Write(BitConverter.GetBytes(inputStream.Length), 0, 8);

            coder.Code(inputStream, outputStream, inputStream.Length, -1, null);

            outputStream.Flush();
            outputStream.Close();
            inputStream.Close();

            return outputStream.ToArray();
        }

        public byte[] Decode(byte[] data)
        {
            var inputStream = new MemoryStream(data);
            var outputStream = new MemoryStream();

            var coder = new Decoder();

            var properties = new byte[5];
            inputStream.Read(properties, 0, 5);

            var fileLengthBytes = new byte[8];
            inputStream.Read(fileLengthBytes, 0, 8);
            var fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(inputStream, outputStream, inputStream.Length, fileLength, null);

            outputStream.Flush();
            outputStream.Close();
            inputStream.Close();

            return outputStream.ToArray();
        }
    }
}