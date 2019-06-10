using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace ToDoList.EntityFramework
{
    public class ListItemDatabaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }
}