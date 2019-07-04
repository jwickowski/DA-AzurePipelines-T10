using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ToDoList.EntityFramework;
using Xunit;

namespace ToDoList.Core.IntegrationTests
{
    public class ListItemCreaterTests
    {
        [Fact]
        public void CheckCreatorAndReader()
        {
            using (var context = PrepareContext())
            {
                CleanDatabase(context);
                var storageSaver = new DbStorage(context);
                var creator = new ListItemCreator(storageSaver);

                creator.Add("a list item");

                var item = context.ToDoListItems.Single();
                Assert.False(item.IsDone);
                Assert.Equal("a list item", item.Name);
            }
        }

        private ToDoListContext PrepareContext()
        {
            var cs = GetTestConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<ToDoListContext>();
            optionsBuilder.UseSqlServer(cs);
            return new ToDoListContext(optionsBuilder.Options);
        }
        
        private string GetTestConnectionString()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");
            var configJson = File.ReadAllText(configPath);
            var resource = JObject.Parse(configJson);
            var connectionStinrg = resource["ConnectionStrings"];
            var cs = connectionStinrg["ToDoListDatabase"];
            var aa = cs.Value<string>();
            return aa;
        }
        
        private void CleanDatabase(ToDoListContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE ToDoListItems");
        }
    }
}