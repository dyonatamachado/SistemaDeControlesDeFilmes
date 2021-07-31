using System;

namespace SistemaDeControleDeFilmes.Models.ViewModels
{
    public class ViewExibicao
    {
        public ViewExibicao(int id, int usuarioId, int filmeId, DateTime dataExibicao)
        {
            Id = id;
            UsuarioId = usuarioId;
            FilmeId = filmeId;
            DataExibicao = dataExibicao;
        }

        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public int FilmeId { get; private set; }
        public DateTime DataExibicao { get; private set; }
    }
}