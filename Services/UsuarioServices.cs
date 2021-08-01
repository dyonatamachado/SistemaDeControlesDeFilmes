using System;
using System.Linq;
using SistemaDeControleDeFilmes.Entities;
using SistemaDeControleDeFilmes.Models.InputModels;
using SistemaDeControleDeFilmes.Persistence;

namespace SistemaDeControleDeFilmes.Services
{
    public class UsuarioServices
    {
        private readonly FilmesContext _dbContext;
        public UsuarioServices(FilmesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Usuario VerificarUsuarioCadastrado(UsuarioInputBase input)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.Cpf == input.CPF);

            return usuario;
        }

        public bool VerificarUsuarioAtivo(UsuarioInputBase input)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.Cpf == input.CPF);

            if(usuario == null)
                return false;
            
            return usuario.Ativo;
        }
    }
}