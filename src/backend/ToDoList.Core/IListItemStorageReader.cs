using System.Collections.Generic;

namespace ToDoList.Core
{
    public interface IListItemStorageReader
    {
        IEnumerable<ListItem> GetAllActiveListItems();
    }
}