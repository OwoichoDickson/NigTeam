namespace NigTeam;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NigTeam.Data;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JwtTokenHelper _jwtTokenHelper;

    public AuthController(ApplicationDbContext context, JwtTokenHelper jwtTokenHelper)
    {
        _context = context;
        _jwtTokenHelper = jwtTokenHelper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserModel userModel)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == userModel.Username && u.Password == userModel.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var token = _jwtTokenHelper.GenerateToken(user.Username, user.Role); // Pass user's role
        return Ok(new { token });
    }


    [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the username already exists
            if (await _context.Users.AnyAsync(u => u.Username == createUserModel.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Create a new user
            var newUser = new UserModel
            {
                Username = createUserModel.Username,
                Password = createUserModel.Password,
                UserId = createUserModel.UserId,
                Role = createUserModel.Role,
               
               
                
                // Other properties...
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User created successfully.");
        }
    }

    public class CreateUserModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
         public int UserId { get; set; }
        // Other properties...
    }



