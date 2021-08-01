using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SistemaDeControleDeFilmes.Entities;
using SistemaDeControleDeFilmes.Models.InputModels;
using SistemaDeControleDeFilmes.Models.ViewModels;
using SistemaDeControleDeFilmes.Persistence;
using SistemaDeControleDeFilmes.Services;

namespace SistemaDeControleDeFilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private readonly FilmesContext _dbContext;
        public FilmesController(FilmesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetFilmes()
        {
            var filmes = _dbContext.Filmes.Where(f => f.Ativo).ToList();
            var filmesViewList = new List<ViewFilme>();
            
            if(filmes.Count == 0)
                return NoContent();

            foreach(var filme in filmes)
            {
                var totalVisualizacoes = _dbContext.Exibicoes
                    .Where(e => e.FilmeId == filme.Id).Count();
            
                var filmeView = new ViewFilme(filme.Id,filme.Titulo,filme.Genero,
                    filme.Sinopse,filme.Ano, totalVisualizacoes);

                filmesViewList.Add(filmeView);
            }

            return Ok(filmesViewList);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetFilmeById(int id)
        {
            var filme = _dbContext.Filmes.SingleOrDefault(f => f.Id == id && f.Ativo);

            if(filme == null)
                return NotFound("Não existe filme cadastrado com este ID ou o filme está Inativo.");
            
            var totalExibicoes = _dbContext.Exibicoes.Where(e => e.FilmeId == id).Count();
            
            var filmeView = new ViewFilme(filme.Id, filme.Titulo, filme.Genero,
                filme.Sinopse, filme.Ano, totalExibicoes);

            return Ok(filmeView);
        }

        [HttpGet("/Filmes/inativos")]
        public IActionResult GetFilmesInativos()
        {
            var filmes = _dbContext.Filmes.Where(f => !f.Ativo).ToList();
            if(filmes.Count == 0)
                return NoContent();

            var filmesViewList = new List<ViewFilme>();

            foreach(var filme in filmes)
            {
                var totalExibicoes = _dbContext.Exibicoes.Where(e => e.FilmeId == filme.Id).Count();

                var filmeView = new ViewFilme(filme.Id, filme.Titulo, filme.Genero,
                filme.Sinopse, filme.Ano, totalExibicoes);

                filmesViewList.Add(filmeView);
            }

            return Ok(filmesViewList);
        }

        [HttpPost]
        public IActionResult PostFilme([FromBody] CadastrarFilme input)
        {
            
            var filmeService = new FilmeServices(_dbContext);
            var filmeJaCadastrado = filmeService.VerificarFilmeJaCadastrado(input);
            var filmeAtivo = filmeService.VerificarFilmeAtivo(input);

            if(filmeJaCadastrado != null)
            {
                if(filmeAtivo)
                    return BadRequest("Já existe filme cadastrado com mesmo título, gênero e ano.");
                else
                    return BadRequest("Já existe filme cadastrado com estes dados porém está inativo.");
            }
            
            var filme = new Filme(input.Titulo, input.Genero, input.Sinopse, input.Ano);
            
            _dbContext.Filmes.Add(filme);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetFilmeById), new {id = filme.Id}, input);
        }

        [HttpPut("{id}")]
        public IActionResult PutFilme(int id, [FromBody] AtualizarFilme input)
        {
            var filme = _dbContext.Filmes.SingleOrDefault(f => f.Id == id && f.Ativo);
            
            var filmeService = new FilmeServices(_dbContext);
            var filmeJaCadastrado = filmeService.VerificarFilmeJaCadastrado(input);

            
            if(filme == null)
                return NotFound("Não existe filme cadastrado com este ID");
            else
            {
                if(filmeJaCadastrado != null && filme.Id != filmeJaCadastrado.Id)
                    return BadRequest($"Os dados inseridos coincidem com outro filme cadastrado no banco de dados com o ID: {filmeJaCadastrado.Id}. Faça a atualização com o Id correto.");
                if(!filme.Ativo)
                    return BadRequest("O filme informado está inativo. Se quiser ativá-lo" +
                     " novamente acesse a rota adequada.");

                filme.AtualizarFilme(input.Titulo, input.Genero, input.Sinopse, input.Ano);
                _dbContext.SaveChanges();
                return NoContent();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchFilme(int id)
        {
            var filme = _dbContext.Filmes.SingleOrDefault(f => f.Id == id);

            if(filme == null)
                return BadRequest("Não existe filme cadastrado com este ID.");
            else
            {
                if(filme.Ativo)
                    return BadRequest("Filme já está Ativo.");
                
                filme.ReativarFilme();
                _dbContext.SaveChanges();

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilme(int id)
        {
            var filme = _dbContext.Filmes.SingleOrDefault(f => f.Id == id);

            if(filme == null)
                return NotFound("Não existe filme cadastrado com este ID.");
            if(!filme.Ativo)
                return BadRequest("Filme já está desativado.");
            
            filme.DesativarFilme();
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}