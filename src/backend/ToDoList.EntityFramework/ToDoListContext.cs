using Microsoft.EntityFrameworkCore;

namespace ToDoList.EntityFramework
{
    public class ToDoListContext: DbContext
    {
        public DbSet<ListItemDatabaseEntity> ToDoListItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Blogging;Integrated Security=True");
        }
    }
}