using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPipeline
{
    class Program
    {
        public static List<Func<RequestDelegate,RequestDelegate>> _List=new List<Func<RequestDelegate, RequestDelegate>>(); 
        static void Main(string[] args)
        {
            Use(next =>
            {
                return Context =>
                {
                    Console.WriteLine("111");
                    return next.Invoke(Context);
                };
            });
            Use(next =>
            {
                return Context =>
                {
                    Console.WriteLine("222");
                    return next.Invoke(Context);
                };
            });

            RequestDelegate end = (context) =>
            {
                Console.WriteLine("end...");
                return Task.CompletedTask;
            };

            //_List.Reverse();
            foreach (var middleware in _List)
            {
                end = middleware.Invoke(end);
            }

            end.Invoke(new Context());
            Console.ReadLine();
        }

        public static void Use(Func<RequestDelegate,RequestDelegate> middleware)
        {
            _List.Add(middleware);
        }
    }
}
