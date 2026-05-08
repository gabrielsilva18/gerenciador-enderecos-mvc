using System.ComponentModel.DataAnnotations;

namespace GerenciadorEnderecos.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
         public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string NomeUsuario { get; set; } = string.Empty;
       
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; } = string.Empty;

        public List<Endereco> Enderecos { get; set; } = new();
    }
}