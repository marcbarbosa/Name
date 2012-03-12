using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Name;

namespace Name.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new NameClient();

            var response = client.Hello();
        }
    }
}
