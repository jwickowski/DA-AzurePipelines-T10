using System;

namespace ToDoList.Storage.Fake
{
    internal class ListItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }
}