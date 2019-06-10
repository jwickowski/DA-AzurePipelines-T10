using System;

namespace ToDoList.Core
{
    public class ListItemChecker
    {
        private IListItemStorageChanger _listItemStorageChanger;

        public ListItemChecker(IListItemStorageChanger listItemStorageChanger)
        {
            _listItemStorageChanger = listItemStorageChanger;
        }

        public void Check(int id)
        {
            _listItemStorageChanger.SetListItemAsDone(id);
        }
    }
}   