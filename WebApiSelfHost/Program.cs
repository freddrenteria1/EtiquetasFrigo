using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:7777"))
            {
                Console.WriteLine("Listo para pesar - con cors");
                Console.WriteLine("Presione cualquier letra para salirse");
                Console.ReadLine();

            }
        }
    }
}
