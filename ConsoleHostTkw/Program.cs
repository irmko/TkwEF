using System;
using System.ServiceModel;

namespace ConsoleHostTkw
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceVersionHost = null;
            try
            {
                Console.WriteLine("Старт сервера ...");
                serviceVersionHost = HelperHost.CreateHost<ServiceHost>();
                Console.WriteLine("Свервер запущен.");
                Console.Read();
                HelperHost.StopService(ref serviceVersionHost);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска: {DateTime.Now.ToString()}{Environment.NewLine}{ex.Message}");
                HelperHost.StopService(ref serviceVersionHost);
            }
        }
    }
}
