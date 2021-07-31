using System;

namespace SistemaDeControleDeFilmes.Entities
{
    // Classe que cria relacionamento entre filme e usuário 
    // representando a exibição do filme
    public class Exibicao
    {
        public Exibicao(int usuarioId, int filmeId)
        {
            UsuarioId = usuarioId;
            FilmeId = filmeId;
            DataExibicao = DateTime.Now;
        }

        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public int FilmeId { get; private set; }
        public DateTime DataExibicao { get; private set; }
    }
}