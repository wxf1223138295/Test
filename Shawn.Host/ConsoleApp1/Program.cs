using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //检测注册表中本地安装的net版本
            string subkeyv4 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
            string subkeyv35 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5";

            bool Isv4 = false;
            bool Isv3 = false;

            Console.WriteLine("11111");

            try
            {
                using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(subkeyv4))
                {
                   
                    if (ndpKey != null)
                    {
                        Console.WriteLine(ndpKey.Name);
                        Isv4 = ndpKey.GetValue("Version").ToString().StartsWith("4");

                    }
                }


                using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(subkeyv35))
                {
                   
                    if (ndpKey != null)
                    {
                        Console.WriteLine(ndpKey.Name);
                        Isv3 = ndpKey.GetValue("Version").ToString().StartsWith("3");

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw;
            }
           

            if (Isv4 && Isv3)
            {
                Console.WriteLine("都装了");
            }
            else
            {
                if (Isv4)
                {
                    Console.WriteLine("4装了");
                }

                if (Isv3)
                {
                    Console.WriteLine("3装了");
                }
            }
            Console.WriteLine("222222222");

            Console.ReadKey();
        }
    }
}
