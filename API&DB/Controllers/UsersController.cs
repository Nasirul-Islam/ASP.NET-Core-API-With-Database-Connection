using API_DB.Models;
using API_DB.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_DB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //private static List<Users> AllUsers = new List<Users>
        //{
        //    new Users { Id = 1, UserName = "Nasirul", Password = "12345" },
        //    new Users { Id = 2, UserName = "Naim", Password = "12345" },
        //    new Users { Id = 3, UserName = "Admin", Password = "1111" },
        //    new Users { Id = 4, UserName = "Admin2", Password = "2222" },
        //    new Users { Id = 5, UserName = "Admin3", Password = "3333" }
        //}; 
        private readonly UsersService _userService;

        public UsersController(UsersService usersService)
        {
            _userService = usersService;
        }


        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult CreateUser(Users user)
        {
            _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.ID }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, Users user)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null) return NotFound();

            user.ID = id;
            _userService.UpdateUser(user);
            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null) return NotFound();

            _userService.DeleteUser(id);
            return NoContent();
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

