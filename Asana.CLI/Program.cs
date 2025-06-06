
using Asana.Library.Models;
using Asana.Library.Services;
using System;

namespace Asana
// my todos: add in comments, record walkthrough video, test project features, make a single global list
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var toDoSvc = ToDoServiceProxy.Current;
            //var toDos = new List<ToDo>();
            var projectsSvc = ProjectServiceProxy.Current;
            int choiceInt;
            
            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. List all ToDos");
                Console.WriteLine("3. List all outstanding ToDos");
                Console.WriteLine("4. Delete a ToDo");
                Console.WriteLine("5. Update a ToDo");
                Console.WriteLine("6. Create a Project");
                Console.WriteLine("7. List all Projects");
                Console.WriteLine("8. Delete a  Project");
                Console.WriteLine("9. Update a  Project");
                Console.WriteLine("10. List all ToDos in a Project");
                Console.WriteLine("11. Exit");

                var choice = Console.ReadLine() ?? "11";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1:
                            Console.Write("Name:");
                            var name = Console.ReadLine();
                            Console.Write("Description:");
                            var description = Console.ReadLine();

                            toDoSvc.AddOrUpdate(new ToDo
                            {
                                Name = name,
                                Description = description,
                                IsCompleted = false,
                                Id = 0
                            });
                            break;
                        case 2:
                            toDoSvc.DisplayToDos(true);
                            break;
                        case 3:
                            toDoSvc.DisplayToDos();
                            break;
                        case 4:
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo to Delete: ");
                            var toDoChoice4 = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDoSvc.GetById(toDoChoice4);
                            toDoSvc.DeleteToDo(reference);
                            break;
                        case 5:
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo to Update: ");
                            var toDoChoice5 = int.Parse(Console.ReadLine() ?? "0");
                            var updateReference = toDoSvc.GetById(toDoChoice5);

                            if (updateReference != null)
                            {
                                Console.Write("Name:");
                                updateReference.Name = Console.ReadLine();
                                Console.Write("Description:");
                                updateReference.Description = Console.ReadLine();
                            }
                            toDoSvc.AddOrUpdate(updateReference);
                            break;
                        case 6:
                            {
                            Console.Write("Name:");
                            name = Console.ReadLine();
                            Console.Write("Description:");
                            description = Console.ReadLine();

                            projectsSvc.AddOrUpdate(new Project
                            {
                                Name = name,
                                Description = description,
                                Id = 0
                            });
                            break;   
                            }
                            
                        case 7:
                            projectsSvc.DisplayProjects();
                            break;
                        case 8:
                            {
                                projectsSvc.DisplayProjects();
                                Console.Write("Project to Delete: ");
                            var projectChoice = int.Parse(Console.ReadLine() ?? "0");

                            var referenceProj = projectsSvc.GetById(projectChoice);
                            projectsSvc.DeleteProject(referenceProj);
                            break;
                            }    
                             
                        case 9:
                            {
                                projectsSvc.DisplayProjects();
                                Console.Write("Project to Update: ");
                                var projectChoice = int.Parse(Console.ReadLine() ?? "0");
                                var updateProj = projectsSvc.GetById(projectChoice);

                                if (updateProj != null)
                                {
                                    Console.Write("Name:");
                                    updateProj.Name = Console.ReadLine();
                                    Console.Write("Description:");
                                    updateProj.Description = Console.ReadLine();
                                }
                                projectsSvc.AddOrUpdate(updateProj);
                                break;
                            }
                        case 10: //start here
                             {
                                projectsSvc.DisplayProjects();
                                Console.Write("Project to list all ToDos: ");
                                var projectChoice = int.Parse(Console.ReadLine() ?? "0");
                                var projectreference = projectsSvc.GetById(projectChoice);

                                projectreference?.ToDos.ForEach(Console.WriteLine);

                            }   
                            break;  
                        case 11:
                            break;            
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                } else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 11);

        }
    }
}