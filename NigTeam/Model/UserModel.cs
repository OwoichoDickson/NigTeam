namespace NigTeam;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserModel
{
   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
   public required int UserId { get; set; }

   public required string Username { get; set; }

    public required string Password { get; set; }

    public required  string Role { get; set; }

  

}
