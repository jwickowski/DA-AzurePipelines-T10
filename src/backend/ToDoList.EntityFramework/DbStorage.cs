using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoList.Core;

namespace ToDoList.EntityFramework
{
    public class DbStorage : IListItemStorageSaver, IListItemStorageChanger, IListItemStorageReader
    {
        private readonly ToDoListContext _context;

        public DbStorage(ToDoListContext context)
        {
            _context = context;
        }

        public ListItem SaveListItem(string listItemName)
        {
            var newItem = new ListItemDatabaseEntity()
            {
                Name = listItemName,
                IsDone = false
            };

            _context.ToDoListItems.Add(newItem);
            _context.SaveChanges();
            
            return new ListItem()
            {
                Id = newItem.Id,
                Name = newItem.Name
            };
        }

        public void SetListItemAsDone(int id)
        {
            var item = _context.ToDoListItems.SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsDone = true;
                _context.SaveChanges();
            }
        }

        public IEnumerable<ListItem> GetAllActiveListItems()
        {
            var result = this._context.ToDoListItems.AsNoTracking()
                .Where(x => x.IsDone == false)
                .Select(x=> new ListItem
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
            return result;
        }
    }
}