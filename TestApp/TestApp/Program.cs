using System;
using System.Threading.Tasks;
using Core;
using System.Threading.Tasks;
using System.Net;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new DefaultFrameworkConstruction().
                UseFileLogger("newlog.txt").
                Build();

            //Task.Run(async () =>
            //{
            //    var result = await WebRequests.PostAsync<SettingsDataModel>("http://localhost:5000/Test", new SettingsDataModel { Id = "From client", Name = "Name2", Value = "20" });

            //    var a = result;
            //});

            Task.Run(async () =>
            {
                await AsyncLock.LockAsync("myKey", () =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine(i + "t1");
                    }
                });
            });

            for (int i = 100; i < 200; i++)
            {
                Console.WriteLine(i + "MAINt");
            }
        }
    }
}
