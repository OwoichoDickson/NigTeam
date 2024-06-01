using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NigTeam.Data;
using NigTeam.Model;

namespace NigTeam.Controller

{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public TeamController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamModel>>> GetTeams()
        {
            if(_dbContext == null){
                return NotFound();
            }
            return await _dbContext.TeamRegister.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamModel>>> GetTeamsById(int Id)
        {
            if(_dbContext == null){
                return NotFound();
            }
            return await _dbContext.TeamRegister.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TeamModel>> GetTeam(int id)
        {
            var team = await _dbContext.TeamRegister.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPost]
         public async Task<ActionResult<TeamModel>> PostTeam(TeamModel team ){
            _dbContext.TeamRegister.Add(team);
           await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeams), new{id = team.Id}, team);

         }



    }
}
