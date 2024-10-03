using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FiotecApi.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A disponibilidade é obrigatória.")]
        public bool Disponibilidade { get; set; }

        [ForeignKey("Autor")]
        [Required(ErrorMessage = "O autor é obrigatório.")]
        public int AutorId { get; set; }
    }
}