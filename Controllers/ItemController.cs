using ToDoAPI_Controller.Models;
using ToDoAPI_Controller.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI_Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private AppDbContext ctx; // ctx is what is used to interact with the DB through EF Core

        /*
         * constructor. Recieves AppDbContext via dependency injection
         */
        public ItemController(AppDbContext context)
        {
            ctx = context;
        }

        /*
         * GET all action
         * takes no parameters
         * returns all items in the database.
         */
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAsync()
        {
            return await ctx.Items.ToListAsync();
        }

        /*
         * GET by Id action
         * takes one parameter: int id, the id of the item to retrieve
         * returns an item from the database based on the id that was passed in.
         * 
         * if the item is null, it means that it does not currently exist in the database, return 404 Not Found.
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetAsync(int id)
        {
            var item = await ctx.Items.FindAsync(id);

            if (item is null) return NotFound();

            return Ok(item);
        }

        /*
         * POST action
         * takes one parameter: Item item, the item that is to be added to the database.
         * returns 201 Ok with the item just added to the database.
         * 
         * if the item that was passed in was null, return 400 Bad Request.
         */
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Item item)
        {
            if(item is null) return BadRequest();

            await ctx.Items.AddAsync(item);
            await ctx.SaveChangesAsync();

            return Ok(item);
        }

        /*
         * PUT action
         * takes two parameters, the Id of the item to update, and an Item with the new 
         *      values to be assigned to the original item.
         * returns 201 Ok with the item that was successfully updated.
         * 
         * if the item retrieved with the id is null, return 404 Not Found.
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Item updateItem)
        {
            var item = await ctx.Items.FindAsync(id);
            if (item is null) return NotFound();

            item.Name = updateItem.Name;
            item.Description = updateItem.Description;
            item.Type = updateItem.Type;
            item.isCompleted = updateItem.isCompleted;

            await ctx.SaveChangesAsync();
            return Ok(item);
        }

        /*
         * DELETE action
         * takes one parameter, the Id of the item to delete. 
         * returns 201 Ok when the item is successfully deleted from the database.
         * 
         * if the item retrieved with the id is null, return 404 Not Found.
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var item = await ctx.Items.FindAsync(id);
            if (item is null) return NotFound();

            ctx.Items.Remove(item);
            await ctx.SaveChangesAsync();

            return Ok();
        }
    }
}
