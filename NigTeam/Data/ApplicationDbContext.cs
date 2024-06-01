using Microsoft.EntityFrameworkCore;
using NigTeam.Model;

namespace NigTeam.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<TeamModel> TeamRegister { get; set; }

    public DbSet<UserModel> Users { get; set; }

}
