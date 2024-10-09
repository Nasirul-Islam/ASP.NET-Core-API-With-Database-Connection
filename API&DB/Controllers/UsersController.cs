using API_DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_DB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<Users> AllUsers = new List<Users>
        {
            new Users { Id = 1, UserName = "Nasirul", Password = "12345" },
            new Users { Id = 2, UserName = "Naim", Password = "12345" },
            new Users { Id = 3, UserName = "Admin", Password = "1111" },
            new Users { Id = 4, UserName = "Admin2", Password = "2222" },
            new Users { Id = 5, UserName = "Admin3", Password = "3333" }
        }; 

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(AllUsers);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = AllUsers.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(Users user)
        {
            user.Id = AllUsers.Count + 1;
            AllUsers.Add(user);
            return CreatedAtAction(nameof(Users), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserInfo(int id, Users updateUser)
        {
            var user = AllUsers.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            user.UserName = updateUser.UserName;
            user.Password = updateUser.Password;
            //return NoContent(); 
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = AllUsers.FirstOrDefault(user => user.Id == id);
            if (user == null) return NotFound();
            AllUsers.Remove(user);
            return Ok(id);
        }

        /*
        This controller provides:

        GET /api/products: Retrieves all products.
        GET /api/products/{id}: Retrieves a single product by ID.
        POST /api/products: Creates a new product.
        PUT /api/products/{id}: Updates an existing product.
        DELETE /api/products/{id}: Deletes a product by ID.
        */
    }
}

