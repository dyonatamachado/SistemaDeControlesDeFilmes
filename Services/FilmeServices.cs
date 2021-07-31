using System;
using System.Linq;
using SistemaDeControleDeFilmes.Entities;
using SistemaDeControleDeFilmes.Models.InputModels;
using SistemaDeControleDeFilmes.Persistence;

namespace SistemaDeControleDeFilmes.Services
{
    public class FilmeServices
    {
        private readonly FilmesContext _dbContext;
        public FilmeServices(FilmesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Filme VerificarFilmeJaCadastrado(FilmeInputBase input)
        {
            var filme = _dbContext.Filmes.SingleOrDefault(f => f.Titulo == input.Titulo 
                && f.Ano == input.Ano);
            
            return filme;
        }

        public bool VerificarFilmeAtivo(FilmeInputBase input)
        {
            var filme = _dbContext.Filmes.SingleOrDefault(f => f.Titulo == input.Titulo 
                && f.Ano == input.Ano);
            
            if(filme == null)
                return false;
        
            return filme.Ativo;
        }
    }
}