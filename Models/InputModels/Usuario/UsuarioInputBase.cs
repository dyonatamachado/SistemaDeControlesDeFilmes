using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeControleDeFilmes.Models.InputModels
{
    public class UsuarioInputBase
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime DataDeNascimento { get; set; }
    }
}