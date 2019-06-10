using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Core;

namespace ToDoList.Storage.Fake
{
    public class FakeStorage : IListItemStorageSaver, IListItemStorageChanger, IListItemStorageReader
    {
        private Dictionary<int, ListItemEntity> listItems;
        private int lastMaxId = 1;
        public FakeStorage()
        {
            listItems = new Dictionary<int, ListItemEntity>();
            var newItem = new ListItemEntity()
            {
                Id =lastMaxId++,
                Name = "test",
                IsDone = false
            };
            listItems.Add(newItem.Id, newItem);
        }

        public ListItem SaveListItem(string listItemName)
        {
            var newItem = new ListItemEntity
            {
                Id = lastMaxId++,
                Name = listItemName,
                IsDone = false
            };
            
            listItems.Add(newItem.Id, newItem);

            return new ListItem()
            {
                Id = newItem.Id,
                Name = newItem.Name
            };
        }

        public void SetListItemAsDone(int id)
        {
            if (listItems.ContainsKey(id))
            {
                var item = listItems[id];
                item.IsDone = true;
            }
        }

        public IEnumerable<ListItem> GetAllActiveListItems()
        {
            var result = this.listItems.Values
                .Where(x => x.IsDone == false)
                .Select(x=> new ListItem()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
            return result;
        }
    }
}