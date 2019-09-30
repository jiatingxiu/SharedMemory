using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedMemoryServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 900 * 720 * 2;
            string name = "SharedMemoryTest";

            Random r = new Random();
            byte[] data = new byte[size];


            var buf = new SharedMemory.CircularBuffer(name, 2, size);
            var bufWrite = new SharedMemory.CircularBuffer(name + "W", 2, size);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int count = buf.Read(data, 0, -1);
                    if (count != 0)
                        Console.WriteLine(data[0]);

                    bufWrite.Write(data);
                }
            });

            Console.ReadKey();
        }
    }
}
