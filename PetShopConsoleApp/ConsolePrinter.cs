using PetShopCompulsory.Core.Entities;
using PetShopCompulsory.Core.ServiceFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopCompulsory.ConsoleApp
{
    public class ConsolePrinter
    {
        readonly IPetService _petService;
        public ConsolePrinter(IPetService petService)
        {
            _petService = petService;
            StartUI();

        }

        void StartUI()
        {
            string[] menuItems =
             {
                "List All Pets",
                "Add Pet",
                "Delete Pet",
                "Edit Pet",
                "5 Cheapest petties",
                "search by pet type",
                "Sort pets by price",
                "Exit"
            };

            var selection = ShowMenu(menuItems);

            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("----------------------------");
                        ShowAllPets();
                        Console.WriteLine("----------------------------");
                        break;
                    case 2:
                        Console.WriteLine("----------------------------");
                        AddPet();
                        Console.WriteLine("----------------------------");
                        break;
                    case 3:
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("Type in the Id of the pet you want to delete: ");
                        DeletePet();
                        Console.WriteLine("----------------------------");
                        break;
                    case 4:
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("edit");
                        EditPet();
                        Console.WriteLine("----------------------------");
                        break;
                    case 5:
                        Console.WriteLine("----------------------------");
                        _petService.GetThe5CheapestPets();
                        Show5PetsByPrice();
                        Console.WriteLine("----------------------------");
                        break;
                    case 6:
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("What type of animal would you like to search for?");
                        string search = Console.ReadLine();
                        _petService.SearchByType(search);
                        PrintSearchPetsByType();
                        Console.WriteLine("----------------------------");
                        break;
                    case 7:
                        Console.WriteLine("----------------------------");
                        _petService.SortByPrice();
                        ShowAllPetsByPrice();
                        Console.WriteLine("----------------------------");
                        break;
                    default:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            Console.WriteLine("Byeee");

            Console.ReadLine();
        }

        private void DeletePet()
        {
            int id;

            while (!int.TryParse(Console.ReadLine(), out id)
                   || id < 1)
            {
                Console.WriteLine("Choose a number bigger than 0");
            }

            _petService.DeletePet(id);
        }

        private void EditPet()
        {
            var petId = PrintFindPetId();

            Console.WriteLine("Name of pet: ");
            var name = Console.ReadLine();

            Console.WriteLine("Pet type: ");
            var type = Console.ReadLine();

            var DateExample = DateTime.Now;
            Console.WriteLine("Pet's BirthDate: (eg 10-10-2010)");
            var birthdate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Day the pet was sold: ");
            var soldDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Color of pet: ");
            var color = Console.ReadLine();

            Console.WriteLine("Previous Owner: ");
            var previousOwner = Console.ReadLine();

            Console.WriteLine("Price of pet: ");
            var price = double.Parse(Console.ReadLine());

            _petService.EditPet(new Pet()
            {
                Id = petId,
                Name = name,
                Type = type,
                Birthdate = birthdate,
                SoldDate = soldDate,
                Color = color,
                PreviousOwner = previousOwner,
                Price = price
            });
        }

        private void AddPet()
        {
            var pet = _petService.GetAPetInstance();

            Console.WriteLine("Name of pet: ");
            pet.Name = Console.ReadLine();

            Console.WriteLine("Pet type: ");
            pet.Type = Console.ReadLine();

            var DateExample = DateTime.Now;
            Console.WriteLine("Pet's BirthDate: (eg 10-10-2010)");
            pet.Birthdate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Day the pet was sold: ");
            pet.SoldDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Color of pet: ");
            pet.Color = Console.ReadLine();

            Console.WriteLine("Previous Owner: ");
            pet.PreviousOwner = Console.ReadLine();

            Console.WriteLine("Price of pet: ");
            pet.Price = double.Parse(Console.ReadLine());

            _petService.AddPet(pet);
        }

        int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("Select What you want to do:\n");

            for (var i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                   || selection < 1
                   || selection > 8)
            {
                Console.WriteLine("Please select a number between 1-8");
            }
            return selection;
        }

        private void ShowAllPets()
        {
            var listOfPets = _petService.GetPets();
            foreach (var pet in listOfPets)
            {
                Console.WriteLine("id: {0}, Name: {1}, Type: {2}\nBirthdate: {3}, SoldDate: {4}, Color: {5}, Previous Owner: {6}, Price {7}",
                    pet.Id, pet.Name, pet.Type, pet.Birthdate, pet.SoldDate, pet.Color, pet.PreviousOwner, pet.Price);
            }
        }

        private void ShowAllPetsByPrice()
        {
            var listOfPets = _petService.SortByPrice();
            foreach (var pet in listOfPets)
            {
                Console.WriteLine("id: {0}, Name: {1}, Type: {2}\nBirthdate: {3}, SoldDate: {4}, Color: {5}, Previous Owner: {6}, Price {7}",
                    pet.Id, pet.Name, pet.Type, pet.Birthdate, pet.SoldDate, pet.Color, pet.PreviousOwner, pet.Price);
            }
        }
        private void Show5PetsByPrice()
        {
            var listOfPets = _petService.GetThe5CheapestPets();
            foreach (var pet in listOfPets)
            {
                Console.WriteLine("id: {0}, Name: {1}, Type: {2}\nBirthdate: {3}, SoldDate: {4}, Color: {5}, Previous Owner: {6}, Price {7}",
                    pet.Id, pet.Name, pet.Type, pet.Birthdate, pet.SoldDate, pet.Color, pet.PreviousOwner, pet.Price);
            }
        }

        private void PrintSearchPetsByType()
        {
            var search = "b";
            var listOfPets = _petService.SearchByType(search);
            foreach (var pet in listOfPets)
            {
                Console.WriteLine("id: {0}, Name: {1}, Type: {2}\nBirthdate: {3}, SoldDate: {4}, Color: {5}, Previous Owner: {6}, Price {7}",
                    pet.Id, pet.Name, pet.Type, pet.Birthdate, pet.SoldDate, pet.Color, pet.PreviousOwner, pet.Price);
            }
        }

        int PrintFindPetId()
        {
            Console.WriteLine("Insert Pet Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number");
            }
            return id;
        }

    }
}
