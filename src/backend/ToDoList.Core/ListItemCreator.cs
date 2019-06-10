using System.Text.RegularExpressions;

namespace ToDoList.Core
{
    public class ListItemCreator
    {
        private IListItemStorageSaver listItemStorageSaver;

        public ListItemCreator(IListItemStorageSaver listItemStorageSaver)
        {
            this.listItemStorageSaver = listItemStorageSaver;
        }

        public ListItem Add(string listItemName)
        {
            var newListItemName = listItemName;
            if (string.IsNullOrWhiteSpace(newListItemName))
            {
                throw new ListItemNameIsRequiredException();
            }

            newListItemName = newListItemName.Trim();
            newListItemName = Regex.Replace(newListItemName, " {2,}", " ");
            return this.listItemStorageSaver.SaveListItem(newListItemName);
        }
    }
}