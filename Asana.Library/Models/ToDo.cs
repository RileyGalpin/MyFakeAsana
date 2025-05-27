using System;

namespace Asana.Library.Models;

public class ToDo
{
    

    public string? Name { get; set; }
    public string? Id { get; set; }

    public string? ProjectId { get; set; }
    

    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public int? Priortity { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Description}";
    }


   
}
