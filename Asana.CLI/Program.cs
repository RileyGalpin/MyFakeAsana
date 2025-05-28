using System;
using System.Collections;
using System.Collections.Immutable;
using System.IO.Compression;
using System.Security.Cryptography;
using Asana.Library.Models;

namespace Asana
// my todos: add in comments, record walkthrough video, test project features, make a single global list
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            var projects = new List<Project>();
            int choiceInt;
            var itemCount = 0;
            var projectCount = 0;
            var toDoChoice = 0;
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
                            {
                                Console.WriteLine("Create Todo in a project? (y/n)");
                                var usrsChoice = Console.ReadLine();
                                if (usrsChoice == "y")
                                {
                                    projects.ForEach(Console.WriteLine);
                                    Console.Write("Project to Create ToDo: ");
                                    var projectChoice = int.Parse(Console.ReadLine() ?? "0");

                                    var projectreference = projects.FirstOrDefault(p => p.Id == projectChoice);
                                    if (projectreference != null)
                                    {
                                        Console.Write("Name:");
                                        var name = Console.ReadLine();
                                        Console.Write("Description:");
                                        var description = Console.ReadLine();
                                        projectreference.ToDos.Add(new ToDo
                                        {
                                            Name = name,
                                            Description = description,
                                            IsCompleted = false,
                                            Id = ++itemCount,
                                        });
                                    }
                                }
                                else
                                {

                                    Console.Write("Name:");
                                    var name = Console.ReadLine();
                                    Console.Write("Description:");
                                    var description = Console.ReadLine();

                                    toDos.Add(new ToDo
                                    {
                                        Name = name,
                                        Description = description,
                                        IsCompleted = false,
                                        Id = ++itemCount 
                                    });
                                }
                            }        
                            break;
                        case 2:
                            Console.WriteLine("General ToDo's: ");
                            toDos.ForEach(Console.WriteLine);
                            Console.WriteLine("Project ToDo's: ");
                            foreach (var project in projects)
                            {
                                Console.WriteLine($"Project: [{project.Id}] {project.Name}");
                                project.ToDos.ForEach(Console.WriteLine);
                            }
                            break;
                        case 3:
                            Console.WriteLine("General ToDo's: ");
                            toDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);

                            Console.WriteLine("Project ToDo's: ");
                            foreach (var project in projects)
                            {
                                Console.WriteLine($"Project: [{project.Id}] {project.Name}");
                                project.ToDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
                            }    
                            break;
                        case 4:
                            Console.WriteLine("General ToDo's: ");
                            toDos.ForEach(Console.WriteLine);
                            Console.WriteLine("Project ToDo's: ");
                            foreach (var project in projects)
                            {
                                Console.WriteLine($"Project: [{project.Id}] {project.Name}");
                                project.ToDos.ForEach(Console.WriteLine);
                            }

                            Console.Write("ToDo to delete: general or project (g/p) ");
                            var usrChoice = Console.ReadLine();
                            if (usrChoice == "p")
                            {
                                Console.WriteLine("Project:");
                                var projectChoice = int.Parse(Console.ReadLine() ?? "0");
                                var projectReference = projects.FirstOrDefault(p => p.Id == projectChoice); 
                                Console.WriteLine("ToDo in Project:");
                                toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                                if (projectReference != null)
                                {
                                    var updateReference = projectReference.ToDos.FirstOrDefault(t => t.Id == toDoChoice);
                                    if (updateReference != null)
                                    {
                                        projectReference.ToDos.Remove(updateReference);
                                    }
                                }
                            }
                            else
                            {

                                Console.Write("ToDo to Delete: ");
                                toDoChoice = int.Parse(Console.ReadLine() ?? "0");

                                var reference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
                                if (reference != null)
                                {
                                    toDos.Remove(reference);
                                }
                            }
                            break;
                        case 5:
                            Console.WriteLine("General ToDo's: ");
                            toDos.ForEach(Console.WriteLine);
                            Console.WriteLine("Project ToDo's: ");
                            foreach (var project in projects)
                            {
                                Console.WriteLine($"Project: [{project.Id}] {project.Name}");
                                project.ToDos.ForEach(Console.WriteLine);
                            }

                            Console.Write("ToDo to Update: general or project (g/p) ");
                            usrChoice = Console.ReadLine();
                            if (usrChoice == "p")
                            {
                                Console.WriteLine("Project:");
                                var projectChoice = int.Parse(Console.ReadLine() ?? "0");
                                var projectReference = projects.FirstOrDefault(p => p.Id == projectChoice); 
                                Console.WriteLine("ToDo in Project:");
                                toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                                if (projectReference != null)
                                {
                                    var updateReference = projectReference.ToDos.FirstOrDefault(t => t.Id == toDoChoice);
                                    if (updateReference != null)
                                    {
                                        Console.Write("Name:");
                                        updateReference.Name = Console.ReadLine();
                                        Console.Write("Description:");
                                        updateReference.Description = Console.ReadLine();
                                    }
                                }
                            } 
                            else
                            {

                                toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                                var updateReference = toDos.FirstOrDefault(t => t.Id == toDoChoice);

                                if (updateReference != null)
                                {
                                    Console.Write("Name:");
                                    updateReference.Name = Console.ReadLine();
                                    Console.Write("Description:");
                                    updateReference.Description = Console.ReadLine();
                                }
                            } 
                            break;
                        case 6:
                            {
                                Console.Write("Project Name:");
                                var projname = Console.ReadLine();
                                Console.Write("Project Description:");
                                var projdescription = Console.ReadLine();

                                projects.Add(new Project
                                {
                                    Name = projname,
                                    Description = projdescription,
                                    Id = ++projectCount,
                                });

                            break;    
                            }
                            
                        case 7:
                            projects.ForEach(Console.WriteLine);
                            break;
                        case 8:
                            {
                                projects.ForEach(Console.WriteLine);
                                Console.Write("Project to Delete: ");
                                var projectChoice = int.Parse(Console.ReadLine() ?? "0");

                                var projectreference = projects.FirstOrDefault(p => p.Id == projectChoice);
                                if (projectreference != null)
                                {
                                    projects.Remove(projectreference);
                                }

                            }    
                            break; 
                        case 9:
                            {
                                projects.ForEach(Console.WriteLine);
                                Console.Write("Project to Update: ");
                                var projectChoice = int.Parse(Console.ReadLine() ?? "0");
                                var updateProject = projects.FirstOrDefault(p => p.Id == projectChoice);

                                if(updateProject != null)
                                {
                                    Console.Write("Name:");
                                    updateProject.Name = Console.ReadLine();
                                    Console.Write("Description:");
                                    updateProject.Description = Console.ReadLine();
                                }
                             }
                            break; 
                        case 10:
                             {
                                projects.ForEach(Console.WriteLine);
                                Console.Write("Project to list all ToDos: ");
                                var projectChoice = int.Parse(Console.ReadLine() ?? "0");

                                var projectreference = projects.FirstOrDefault(p => p.Id == projectChoice);
                                if (projectreference != null)
                                {
                                    projectreference.ToDos.ForEach(Console.WriteLine);
                                }

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