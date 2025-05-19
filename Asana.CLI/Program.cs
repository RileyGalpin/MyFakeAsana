using System;
using System.Security.Cryptography;
using Asana.Library.Models;

namespace Asana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<ToDo> toDos = new List<ToDo>();
            var choiceInt = 0;

            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. Exit");

                var choice = Console.ReadLine() ?? "2";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1:
                            var toDo = new ToDo();
                            Console.WriteLine("Name: ");
                            toDo.Name = Console.ReadLine();
                            Console.WriteLine("Description: ");
                            toDo.Description = Console.ReadLine();

                            toDos.Add(toDo);
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;

                    }
                }
                else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection."); 
                }

                if (toDos.Any()) //only get last item if there is an item in the list
                {
                    Console.WriteLine(toDos.Last());
                }
            } while(choiceInt != 2);
        }


    }



}