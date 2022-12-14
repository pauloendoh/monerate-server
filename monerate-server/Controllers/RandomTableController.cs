using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using monerate_server.Data;
using monerate_server.Models;

namespace monerate_server.Controllers
{
    [ApiController]
    public class RandomTableController : ControllerBase
    {
        private readonly MyDbContext _db;
        public RandomTableController(MyDbContext db)
        {
            _db = db;
        }
        
      
        // create an endpoint that creates a random-table and returns that saved random table
        [HttpGet]
        [Route("/random-table")]
        async public Task<IActionResult> CreateAndGet()
        {
            var randomTable = new RandomTable();
            await _db.RandomTable.AddAsync(randomTable);
            await _db.SaveChangesAsync();


            return Ok(randomTable);

        }
    }
}
