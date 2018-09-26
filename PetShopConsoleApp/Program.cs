using Microsoft.Extensions.DependencyInjection;
using PetShopCompulsory.ConsoleApp;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.DomainService.impl;
using PetShopCompulsory.Core.ServiceFolder;
using PetShopCompulsory.Core.ServiceFolder.implementation;
using PetShopCompulsory.Infrastructure.Data;
using System;

namespace PetShopConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hiyo");

            FakeData.InitData();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var petService = serviceProvider.GetRequiredService<IPetService>();
            new ConsolePrinter(petService);
            
            ///*////then build provider 
            //var serviceProvider = serviceCollection.BuildServiceProvider();
            //var printer = serviceProvider.GetRequiredService<IPrinter>();
            //printer.StartUI();*/
            Console.ReadLine();
        }
    }
}
