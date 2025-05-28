using System;

namespace Asana.Library.Models;

public class Project
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double CompletePercent {
        get
        {
            if (ToDos == null || ToDos.Count == 0)
            {
                return 0.0;
            }
            else
            {
                var completedCount = ToDos.Count(t => t.IsCompleted == true);
                return Math.Round((double)completedCount / ToDos.Count * 100, 2);
            }

            
        }
    }

        public List <ToDo> ToDos{ get; set; } = new List<ToDo>();


        public int Id { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Description} ({CompletePercent}% Complete, {ToDos.Count} ToDos)";
        }
    }
