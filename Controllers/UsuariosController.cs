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
    public class UsuariosController : ControllerBase
    {
        private readonly FilmesContext _dbContext;
        public UsuariosController(FilmesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _dbContext.Usuarios.Where(u => u.Ativo).ToList();

            if(usuarios.Count() == 0)
                return NoContent();

            var usuarioViewList = new List<ViewUsuario>();

            foreach(var u in usuarios)
            {
                var qtdExibicoes =_dbContext.Exibicoes.Where(e => e.UsuarioId == u.Id).ToList().Count();

                var usuarioView = new ViewUsuario(u.Id, u.Nome, u.Cpf, u.DataDeCadastro, qtdExibicoes);

                usuarioViewList.Add(usuarioView);
            }

            return Ok(usuarioViewList);
        }

        [HttpGet("/Usuarios/inativos")]
        public IActionResult GetUsuariosInativos()
        {
            var usuarios = _dbContext.Usuarios.Where(u => !u.Ativo).ToList();

            if(usuarios.Count() == 0)
                return NoContent();

            var usuarioViewList = new List<ViewUsuario>();

            foreach(var u in usuarios)
            {
                var qtdExibicoes =_dbContext.Exibicoes.Where(e => e.UsuarioId == u.Id).ToList().Count();

                var usuarioView = new ViewUsuario(u.Id, u.Nome, u.Cpf, u.DataDeCadastro, qtdExibicoes);

                usuarioViewList.Add(usuarioView);
            }
            
            return Ok(usuarioViewList);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.Id == id);
            int qtdFilmes;

            if(usuario == null)
                qtdFilmes = 0;
            else
                qtdFilmes = _dbContext.Exibicoes.Where(e => e.UsuarioId == usuario.Id).ToList().Count;

            if(usuario == null)
                return NotFound();
            else
            {
                if(!usuario.Ativo)
                    return NotFound("Não existe usuário ativo com este ID.");
                
                var usuarioView = new ViewUsuario(usuario.Id, usuario.Nome,
                    usuario.Cpf, usuario.DataDeCadastro, qtdFilmes);

                return Ok(usuarioView);
            }
        }

        [HttpPost]
        public IActionResult PostUsuario([FromBody] CadastrarUsuario input)
        {
            var usuarioService = new UsuarioServices(_dbContext);
            var usuarioJaCadastrado = usuarioService.VerificarUsuarioCadastrado(input);

            if(usuarioJaCadastrado)
            {
                var usuarioAtivo = usuarioService.VerificarUsuarioAtivo(input);

                if(usuarioAtivo)
                    return BadRequest("Já existe usuário cadastrado com este CPF.");
                else
                    return BadRequest("Já existe usuário cadastrado com este CPF porém está inativo.");
            }

            var usuarioContext = _dbContext.Usuarios;
            var usuario = new Usuario(input.Nome, input.CPF, input.DataDeNascimento);
            
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetUsuarioById), new {id = usuario.Id}, input);
        }

        [HttpPut("{id}")]
        public IActionResult PutUsuario(int id, [FromBody] AtualizarUsuario input)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.Id == id);

            if(usuario == null)
                return BadRequest("Não existe usuário cadastrado com este ID.");
            
            if(!usuario.Ativo)
                return BadRequest("Usuário está inativo.");
            
            usuario.AtualizarUsuario(input.Nome, input.CPF, input.DataDeNascimento);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUsuario(int id)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.Id == id);
            
            if(usuario == null)
                return BadRequest("Não existe usuário cadastrado com este ID.");
            
            if(usuario.Ativo)
                return BadRequest("Usuário já está ativo.");

            usuario.ReativarUsuario();
            _dbContext.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _dbContext.Usuarios.SingleOrDefault(u => u.Id == id);
            
            if(usuario == null)
                return BadRequest("Não existe usuário cadastrado com este ID.");
            
            if(!usuario.Ativo)
                return BadRequest("Usuário já está inativo.");

            usuario.DesativarUsuario();
            _dbContext.SaveChanges();
            
            return NoContent();
        }
    }
}