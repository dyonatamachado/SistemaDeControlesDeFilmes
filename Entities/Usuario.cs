using System;
using System.Collections.Generic;

namespace SistemaDeControleDeFilmes.Entities
{
    public class Usuario
    {
        public Usuario(string nome, string cpf, DateTime dataDeNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
            DataDeCadastro = DateTime.Today;
            Ativo = true;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public DateTime DataDeCadastro { get; private set; }
        public bool Ativo { get; private set; }
        public IEnumerable<Exibicao> Exibicoes { get; private set; }

        public void DesativarUsuario()
        {
            Ativo = false;
        }

        public void AtualizarUsuario(string nome, string cpf, DateTime dataDeNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
        }

        public void ReativarUsuario()
        {
            Ativo = true;
        }
    }
}