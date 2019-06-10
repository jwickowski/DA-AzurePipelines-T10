using System;

namespace ToDoList.Core
{
    public interface IListItemStorageChanger
    {
        void SetListItemAsDone(int id);
    }
}