using System;
using System.Collections.Generic;

namespace ToDoList.Core
{
    public class ListItemReader
    {
        private IListItemStorageReader listItemStorageReader;

        public ListItemReader(IListItemStorageReader listItemStorageReader)
        {
            this.listItemStorageReader = listItemStorageReader;
        }

        public IEnumerable<ListItem> GetAll()
        {
            return listItemStorageReader.GetAllActiveListItems();
        }
    }
}