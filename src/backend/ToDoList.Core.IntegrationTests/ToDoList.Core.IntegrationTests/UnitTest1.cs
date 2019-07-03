using System;
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
            var cs = GetTestConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<ToDoListContext>();
            optionsBuilder.UseSqlServer(cs);
            using (var context = new ToDoListContext())
            {
                var storageSaver = new DbStorage(context);
                var creator = new ListItemCreator(storageSaver);
                var item = creator.Add("a list item");
                
            }
        }

        private string GetTestConnectionString()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");
            var configJson = File.ReadAllText(configPath);
            var resource = JObject.Parse(configJson);
            var csss = resource["ConnectionStrings"];
            var connectionStinrg = resource["ConnectionStrings"];
            var cs = connectionStinrg["ToDoListDatabase"];
            var aa = cs.Value<string>();
            return aa;
        }
    }
}