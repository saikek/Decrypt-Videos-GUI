using System.IO;

namespace DecryptPluralSightVideos.Encryption
{
    public class PsStream : IPsStream
    {
        private readonly Stream fileStream;
        private long _length;

        public long Length
        {
            get
            {
                return _length;
            }
        }

        public int BlockSize
        {
            get
            {
                return 262144;
            }
        }

        public PsStream(string filenamePath)
        {
            fileStream = new FileStream(filenamePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            _length = new FileInfo(filenamePath).Length;
        }

        public void Seek(int offset, SeekOrigin begin)
        {
            if (_length <= 0L)
                return;
            fileStream.Seek((long)offset, begin);
        }

        public int Read(byte[] pv, int i, int count)
        {
            if (_length <= 0L)
                return 0;
            return fileStream.Read(pv, i, count);
        }

        public void Dispose()
        {
            _length = 0L;
            fileStream.Dispose();
        }
    }
}