using System.ComponentModel.DataAnnotations;

namespace SistemaDeControleDeFilmes.Models.InputModels
{
    public class CadastrarExibicao
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int FilmeId { get; set; }
    }
}