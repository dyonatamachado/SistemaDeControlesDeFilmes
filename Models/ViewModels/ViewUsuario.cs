using System;

namespace SistemaDeControleDeFilmes.Models.ViewModels
{
    public class ViewUsuario
    {
        public ViewUsuario(int id, string nome, string cPF, DateTime dataDeCadastro, int filmesAssistidos)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
            DataDeCadastro = dataDeCadastro;
            FilmesAssistidos = filmesAssistidos;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataDeCadastro { get; private set; }
        public int FilmesAssistidos { get; private set; }
    }
}