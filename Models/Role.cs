using System.ComponentModel.DataAnnotations;

namespace Sign.Models
{

    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Ex : "Admin", "User".

        // Relation avec les utilisateurs
        public ICollection<User> Users { get; set; }
    }
}
