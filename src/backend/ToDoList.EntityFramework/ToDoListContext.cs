using Microsoft.EntityFrameworkCore;

namespace ToDoList.EntityFramework
{
    public class ToDoListContext: DbContext
    {
        public DbSet<ListItemDatabaseEntity> ToDoListItems { get; set; }

        public ToDoListContext()
        {
            
        }
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
        {
            
        }
        
    }
}