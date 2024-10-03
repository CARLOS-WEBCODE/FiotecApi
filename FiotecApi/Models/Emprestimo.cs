namespace FiotecApi.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public int UsuarioId { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}