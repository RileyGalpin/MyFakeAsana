using System;

namespace Asana.Library.Models;

public class ToDo
{
    

    public string? Name { get; set;}
    

    public string? description { get; set; }
    public bool? isDone { get; set; }
    public int? priotity { get; set; }

    public ToDo()
    {
        
    }

 
   
}
