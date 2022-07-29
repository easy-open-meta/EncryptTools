using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LockedUtil;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Encrypt encrypt = new Encrypt();
            var oldStr = "15019927415";
            var newStr = encrypt.EncryptStr(oldStr);
            Console.WriteLine(newStr);
            var sourceStr = encrypt.DeEncryptStr(newStr);
            Console.WriteLine(sourceStr);
            Console.ReadKey();
        }

    }
}
