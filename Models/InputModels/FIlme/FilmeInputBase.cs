using System.ComponentModel.DataAnnotations;

namespace SistemaDeControleDeFilmes.Models.InputModels
{
    public class FilmeInputBase
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Genero { get; set; }
        [Required]
        public string Sinopse { get; set; }
        [Required]
        public int Ano { get; set; }
    }
}