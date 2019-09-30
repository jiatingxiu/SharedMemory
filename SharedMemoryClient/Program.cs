using SharedMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedMemoryClient
{
    class Program
    {
        private static AutoResetEvent _Signal = new AutoResetEvent(false);
        static byte[] data;
        static void Main(string[] args)
        {
            int size = 900 * 720 * 2;
            string name = "SharedMemoryTest";

            data = new byte[size];

            var buf = new SharedMemory.CircularBuffer(name);
            bufW = new SharedMemory.CircularBuffer(name + "W");
            int i = 0;

            Task.Factory.StartNew(Read);

            while (true)
            {
                data[0] = (byte)Console.ReadKey().Key;
                int result = buf.Write(data);

                _Signal.WaitOne();

                Console.WriteLine("========================" + data[0]);
            }

            Console.ReadKey();
        }

        static CircularBuffer bufW;

        private static void Read()
        {
            while (true)
            {
                int count = bufW.Read(data, 0, -1);
                _Signal.Set();
            }
        }
    }
}
