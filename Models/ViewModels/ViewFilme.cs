namespace SistemaDeControleDeFilmes.Models.ViewModels
{
    public class ViewFilme
    {
        public ViewFilme(int id, string titulo, string genero, string sinopse, int ano, int totalDeEspectadores)
        {
            Id = id;
            Titulo = titulo;
            Genero = genero;
            Sinopse = sinopse;
            Ano = ano;
            TotalDeEspectadores = totalDeEspectadores;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Genero { get; private set; }
        public string Sinopse { get; private set; }
        public int Ano { get; private set; }
        public int TotalDeEspectadores { get; private set; }
    }
}