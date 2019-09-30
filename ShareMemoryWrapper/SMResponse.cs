using SharedMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMemoryWrapper
{
    public class SMResponse
    {
        private CircularBuffer _CircularBuffer;

        private byte[] _ReadCache;

        public event Action<byte[]> ClientMessage = null;

        public SMResponse(string name, int nodeCount, int nodeBufferSize)
        {
            _ReadCache = new byte[nodeBufferSize];

            _CircularBuffer = new CircularBuffer(name, nodeCount, nodeBufferSize);

            Task.Factory.StartNew(Read);
        }

        private void Read()
        {
            while (true)
            {
                int count = _CircularBuffer.Read(_ReadCache);
                if (count != 0)
                {
                    ClientMessage?.Invoke(_ReadCache);
                }
            }
        }
    }
}
