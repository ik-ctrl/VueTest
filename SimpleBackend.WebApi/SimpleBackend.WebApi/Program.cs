using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SimpleBackend.WebApi
{
    /// <summary>
    /// Главный класс приложения
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Запуск приложения
        /// </summary>
        /// <param name="args">Параметры командной строки</param>
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Создания строителя приложения
        /// </summary>
        /// <param name="args">Параметры командной строки</param>
        /// <returns>Строитель приложения</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(option =>
                    {
                        option.ListenAnyIP(5000);
                    });
                });
    }
}