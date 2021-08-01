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
    public class ExibicoesController : ControllerBase
    {
        private readonly FilmesContext _dbContext;
        public ExibicoesController(FilmesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetExibicoes()
        {
            var exibicoes = _dbContext.Exibicoes.ToList();
            var exibicoesViewList = new List<ViewExibicao>();

            if(exibicoes.Count == 0)
                return NoContent();
            
            foreach(var exibicao in exibicoes)
            {
                var exibicaoView = new ViewExibicao(exibicao.Id, exibicao.UsuarioId, 
                    exibicao.FilmeId, exibicao.DataExibicao);
                
                exibicoesViewList.Add(exibicaoView);
            }
            return Ok(exibicoesViewList);
        }

        [HttpGet("{id}")]
        public IActionResult GetExibicaoById(int id)
        {
            var exibicao = _dbContext.Exibicoes.SingleOrDefault(e => e.Id == id);

            if(exibicao == null)
                return NotFound("Não existe exibição de filme cadastrada com este ID.");
            
            var exibicaoView = new ViewExibicao(exibicao.Id, exibicao.UsuarioId, exibicao.FilmeId, exibicao.DataExibicao);
            return Ok(exibicaoView);
        }

        [HttpGet("UsuarioId/{usuarioId}")]
        public IActionResult GetExibicoesByUsuarioId(int usuarioId)
        {
            var exibicoes = _dbContext.Exibicoes.Where(e => e.UsuarioId == usuarioId).ToList();
            var exibicoesViewList = new List<ViewExibicao>();
            if(exibicoes.Count == 0)
                return NotFound("Este usário não assistiu nenhum filme até o momento.");
            
            foreach (var exibicao in exibicoes)
            {
                var exibicaoView = new ViewExibicao(exibicao.Id, exibicao.UsuarioId, 
                    exibicao.FilmeId, exibicao.DataExibicao);
                
                exibicoesViewList.Add(exibicaoView);
            }

            return Ok(exibicoesViewList);
        }

        [HttpGet("FilmeId/{filmeId}")]
        public IActionResult GetExibicoesByFilmeId(int filmeId)
        {
            var exibicoes = _dbContext.Exibicoes.Where(e => e.FilmeId == filmeId).ToList();
            var exibicoesViewList = new List<ViewExibicao>();
            if(exibicoes.Count == 0)
                return NotFound("Este usário não assistiu nenhum filme até o momento.");
            
            foreach (var exibicao in exibicoes)
            {
                var exibicaoView = new ViewExibicao(exibicao.Id, exibicao.UsuarioId, 
                    exibicao.FilmeId, exibicao.DataExibicao);
                
                exibicoesViewList.Add(exibicaoView);
            }

            return Ok(exibicoesViewList);
        }

        [HttpPost]
        public IActionResult PostExibicao([FromBody] CadastrarExibicao input)
        {
            var exibicaoService = new ExibicaoServices(_dbContext);
            
            var exibicaoJaCadastrada = exibicaoService.VerificarExibicaoJaCadastrada(input.UsuarioId, input.FilmeId);
            if(exibicaoJaCadastrada)
                return BadRequest("Este filme já consta no histórico de exibições deste usuário.");
            
            var usuarioValido = exibicaoService.UsuarioValido(input.UsuarioId);
            if(!usuarioValido)
                return NotFound("O usuário informado não está cadastrado ou está inativo.");

            var filmeValido = exibicaoService.FilmeValido(input.FilmeId);
            if(!filmeValido)
                return NotFound("O filme informado não está cadastrado ou está inativo.");
            
            var exibicao = new Exibicao(input.UsuarioId, input.FilmeId);

            _dbContext.Exibicoes.Add(exibicao);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetExibicaoById), new {id = exibicao.Id}, input);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExibicao(int id)
        {
            var exibicao = _dbContext.Exibicoes.SingleOrDefault(e => e.Id == id);

            if(exibicao == null)
                return NotFound("Não existe exibição cadastrada com este ID.");
            
            _dbContext.Exibicoes.Remove(exibicao);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}