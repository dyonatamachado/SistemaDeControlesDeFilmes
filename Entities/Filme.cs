using System.Collections.Generic;

namespace SistemaDeControleDeFilmes.Entities
{
    public class Filme
    {
        public Filme(string titulo, string genero, string sinopse, int ano)
        {
            Titulo = titulo;
            Genero = genero;
            Sinopse = sinopse;
            Ano = ano;
            Ativo = true;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Genero { get; private set; }
        public string Sinopse { get; private set; }
        public int Ano { get; private set; }
        public bool Ativo { get; private set; }
        public IEnumerable<Exibicao> Exibicoes { get; private set; }

        public void DesativarFilme()
        {
            Ativo = false;
        }

        public void AtualizarFilme(string titulo, string genero, string sinopse, int ano)
        {
            Titulo = titulo;
            Genero = genero;
            Sinopse = sinopse;
            Ano = ano;
        }

        public void ReativarFilme()
        {
            Ativo = true;
        }
    }
}