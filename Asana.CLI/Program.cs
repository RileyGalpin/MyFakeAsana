using System;
using System.Collections;
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
                Console.WriteLine("2. Delete a ToDo");
                Console.WriteLine("3. Update a ToDo");
                Console.WriteLine("4. List all ToDos");
                Console.WriteLine("5. Create a project");
                Console.WriteLine("6. Delete a project");
                Console.WriteLine("7. Update a project");
                Console.WriteLine("8. List all projects");
                Console.WriteLine("9. List all ToDos in a Given Project");
                Console.WriteLine("10. Exit");

                var choice = Console.ReadLine() ?? "10";

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
                            toDo.IsCompleted = false;

                            toDos.Add(toDo);
                            break;
                        case 2:
                            Console.WriteLine("Which ToDo should be deleted? Please specify the name");
                            var toRemove = Console.ReadLine();
                            var item = toDos.FirstOrDefault(todo => todo.Name == toRemove);
                            if (item != null)
                            {
                                toDos.Remove(item);
                                Console.WriteLine("ToDo removed.");
                            }
                            else
                            {
                                Console.WriteLine("ToDo not found.");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Which ToDo should be Updated? Please specify the name");
                            var toUpdate = Console.ReadLine();
                            var change = toDos.FirstOrDefault(todo => todo.Name == toUpdate);
                            if (change != null)
                            {
                                bool updating = true;
                                while (updating)
                                {
                                    Console.WriteLine("What would you like to update? Please Input a number");
                                    Console.WriteLine("1. Name");
                                    Console.WriteLine("2. Description");
                                    Console.WriteLine("3. Priority"); //what
                                    Console.WriteLine("4. IsComplete");
                                    Console.WriteLine("5. Finished update");
                                    var selectionInt = 0;

                                    var selection = Console.ReadLine() ?? "5";

                                    if (int.TryParse(selection, out selectionInt))
                                    {
                                        switch (selectionInt)
                                        {
                                            case 1:
                                                Console.WriteLine("Input new name");
                                                change.Name = Console.ReadLine();
                                                break;
                                            case 2:
                                                Console.WriteLine("Input new description");
                                                change.Description = Console.ReadLine();
                                                break;
                                            case 3:
                                                int newpriority = 0;
                                                Console.WriteLine("Input new priority"); //how to do this
                                                var priority = Console.ReadLine();
                                                if (int.TryParse(priority, out newpriority))
                                                {
                                                    change.Priortity = newpriority;
                                                }
                                                
                                                break;
                                            case 4:
                                                if (change.IsCompleted == true)
                                                {
                                                    change.IsCompleted = false;
                                                    Console.WriteLine("ToDo marked as incomplete");
                                                }
                                                else if (change.IsCompleted == false)
                                                {
                                                    change.IsCompleted = true;
                                                    Console.WriteLine("ToDo marked as complete");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("ToDo in invalid state");
                                                }
                                                
                                                break;
                                            case 5:
                                                Console.WriteLine("Exiting update");
                                                updating = false;
                                                break;
                                            default:
                                                Console.WriteLine("ERROR: Unknown menu selection");
                                                break;



                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine($"ERROR: {selection} is not a valid menu selection.");
                                    }
                                }
                                                        
                            }
                            else
                            {
                                Console.WriteLine("ToDo not found.");
                            }
                            break;
                        case 4:
                           
                            if (toDos.Count > 0)
                            {
                                foreach (var todo in toDos)
                                {
                                    Console.WriteLine($"Name: {todo.Name}");
                                    Console.WriteLine($"Description: {todo.Description}");
                                    Console.WriteLine($"Priority: {todo.Priortity}");
                                    Console.WriteLine($"Id: {todo.Id}");
                                    Console.WriteLine($"Completed?: {todo.IsCompleted}");
                                    Console.WriteLine("");

                                }

                            }
                            else
                            {
                                Console.WriteLine("No ToDos to List.");
                            }
                            break;
                        case 5:

                            break;
                        case 6:

                            break;
                        case 7:

                            break; 
                        case 8:

                            break;
                        case 9:

                            break;           
                        case 10:
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