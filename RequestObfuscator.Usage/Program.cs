using System;

namespace RequestObfuscator.Usage
{
    class Demo
    {
        static void Main(string[] args)
        {
            RequestObfuscator.Instance.Start();

            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}