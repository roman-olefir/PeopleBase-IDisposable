using System;
using System.Collections.Generic;
using System.Configuration;

namespace People_Base
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("МЕНЮ:");
            Console.WriteLine("1. Додати в базу людину");
            Console.WriteLine("2. Переглянути інформацію про людину");
            Console.Write("----->");

            var inputMenu = Convert.ToInt32(Console.ReadLine());
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (DataAccess dataBase = new DataAccess(connectionString))
            {
                if (inputMenu == 1)
                {
                    AddNewPeople(dataBase);
                }
                else if (inputMenu == 2)
                {
                    ShowPeople(dataBase);
                }
            }
            Console.ReadKey();
        }

        static void ShowPeople(DataAccess dataBase)
        {
            List<string> list = dataBase.GetAll();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            Console.WriteLine("Enter name: ");
            var peopleShowInput = Console.ReadLine();

            People peopleInfo = dataBase.Get(peopleShowInput);
            Console.WriteLine("FullName: {0}\nGender: {1}\nBirthday: {2}\nAge: {3}", peopleInfo.FullName, peopleInfo.Gender, peopleInfo.BirthDay, peopleInfo.Age);

            int carCount = 1;
            foreach (var carInfo in peopleInfo.Cars)
            {
                Console.WriteLine("Car number {0}:\nmodel: {1} {2}\ncolor: {3}", carCount, carInfo.Brand, carInfo.Model, carInfo.Color);
                Console.WriteLine();
                carCount++;
            }
        }

        static void AddNewPeople(DataAccess dataBase)
        {
            People peopleInfo = new People();
            Console.Write("Enter full name: ");
            peopleInfo.FullName = Console.ReadLine();
            Console.Write("Enter gender: ");
            peopleInfo.Gender = Console.ReadLine();
            Console.Write("Enter birthday(dd.mm.yyyy): ");
            string birthDay = Console.ReadLine();
            string[] dataTime = birthDay.ToString().Split('.');
            peopleInfo.BirthDay = new DateTime(Convert.ToInt32(dataTime[2]), Convert.ToInt32(dataTime[1]), Convert.ToInt32(dataTime[0]));

            peopleInfo.Cars = new List<Car>();
            Console.Write("Enter number of cars: ");
            int carCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < carCount; i++)
            {
                Car carInfo = new Car();
                Console.Write("Enter brand of your car: ");
                carInfo.Brand = Console.ReadLine();
                Console.Write("Enter model this car: ");
                carInfo.Model = Console.ReadLine();
                Console.Write("Enter color this car: ");
                carInfo.Color = Console.ReadLine();
                peopleInfo.Cars.Add(carInfo);
            }
            dataBase.Add(peopleInfo);
        }
    }
}