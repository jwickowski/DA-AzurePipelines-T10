using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core;

namespace ToDoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemsController : ControllerBase
    {
        private ListItemReader listItemReader;
        private ListItemCreator listItemCreator;
        private ListItemChecker listItemChecker;

        public ListItemsController(
            ListItemReader listItemReader, 
            ListItemCreator listItemCreator, 
            ListItemChecker listItemChecker)
        {
            this.listItemReader = listItemReader;
            this.listItemCreator = listItemCreator;
            this.listItemChecker = listItemChecker;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ListItem>> Get()
        {
            var result =  this.listItemReader.GetAll();
            return new ActionResult<IEnumerable<ListItem>>(result);
        }

   
        [HttpPost]
        public ListItem Post([FromBody] PostListItemModel postListItemModel)
        {
            return this.listItemCreator.Add(postListItemModel.Name);
        }

        [HttpPut]
        [Route("{id}/check")]
        public void Check(int id)
        {
            this.listItemChecker.Check(id);
        }
    }
}