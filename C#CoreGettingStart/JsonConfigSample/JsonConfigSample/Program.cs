using System;
using Microsoft.Extensions.Configuration;

namespace JsonConfigSample
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("class.json");

            var configuration = builder.Build();
            //configuration[]
        }
    }
}
