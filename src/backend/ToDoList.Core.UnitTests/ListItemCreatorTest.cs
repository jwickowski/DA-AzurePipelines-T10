using NSubstitute;
using Xunit;

namespace ToDoList.Core.UnitTests
{
    public class ListItemCreatorTest
    {
        IListItemStorageSaver listItemStorageSaver;
        ListItemCreator listItemCreator;

        public ListItemCreatorTest()
        {
            listItemStorageSaver = Substitute.For<IListItemStorageSaver>();
            listItemCreator = new ListItemCreator(listItemStorageSaver);
        }

        private void Act(string listItemName)
        {
            listItemCreator.Add(listItemName);
        }

        [Fact]
        public void when_add_list_item_then_save_it()
        {
            Act("ItemName");
            listItemStorageSaver.Received().SaveListItem("ItemName");
        }

        [Fact]
        public void when_add_list_item_with_no_or_empty_name_then_throw_NameIsRequiredException()
        {
            Assert.Throws<ListItemNameIsRequiredException>(() => { Act(""); });
            Assert.Throws<ListItemNameIsRequiredException>(() => { Act("    "); });
            Assert.Throws<ListItemNameIsRequiredException>(() => { Act(null); });
            Assert.Throws<ListItemNameIsRequiredException>(() => { Act("    "); });
        }

        [Theory]
        [InlineData("a ", "a")]
        [InlineData(" a", "a")]
        [InlineData(" a ", "a")]
        [InlineData("  a  ", "a")]
        [InlineData("a  a", "a a")]
        public void when_add_list_item_then_trim_multiple_spaces(string listItemNameInput, string expected)
        {
            Act(listItemNameInput);
            listItemStorageSaver.Received().SaveListItem(expected);
        }
    }
}