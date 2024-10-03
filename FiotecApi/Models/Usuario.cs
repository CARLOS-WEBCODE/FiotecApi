using System.ComponentModel.DataAnnotations;

namespace FiotecApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }
        public ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
    }
}