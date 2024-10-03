using System.ComponentModel.DataAnnotations;

namespace FiotecApi.Models
{
    public class Autor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}