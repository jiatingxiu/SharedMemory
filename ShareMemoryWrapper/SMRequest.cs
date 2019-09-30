using SharedMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMemoryWrapper
{
    public class SMRequest
    {
        private CircularBuffer _CircularBuffer;

        public SMRequest(string name)
        {
            _CircularBuffer = new CircularBuffer(name);
        }

        public int Write(byte[] source, int startIndex = 0, int timeout = 1000)
        {
            return Write(source, startIndex, timeout);
        }
    }
}
