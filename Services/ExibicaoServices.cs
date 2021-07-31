using System.Linq;
using SistemaDeControleDeFilmes.Persistence;

namespace SistemaDeControleDeFilmes.Services
{
    public class ExibicaoServices
    {
        private readonly FilmesContext _dbContext;
        public ExibicaoServices(FilmesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool VerificarExibicaoJaCadastrada(int usuarioId, int filmeId)
        {
            var exibicao = _dbContext.Exibicoes.SingleOrDefault(e => e.UsuarioId == usuarioId && e.FilmeId == filmeId);

            if(exibicao == null)
                return false;
            else
                return true;
        }

        public bool UsuarioValido(int usuarioId)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.Id == usuarioId && u.Ativo);

            if(usuario == null)
                return false;
            else
                return true;
        }

        public bool FilmeValido(int filmeId)
        {
            var filme = _dbContext.Filmes.SingleOrDefault(f => f.Id == filmeId && f.Ativo);

            if(filme == null)
                return false;
            else
                return true;
        }
    }
}