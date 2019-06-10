namespace ToDoList.Core
{
    public interface IListItemStorageSaver
    {
        ListItem SaveListItem(string listItemName);
    }
}