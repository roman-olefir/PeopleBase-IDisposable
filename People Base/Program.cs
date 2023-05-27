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
            dataBase.Add(peopleInfo);
        }
    }
}
