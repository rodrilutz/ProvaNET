using System;
using MundoDoCinema.WebApi.Dominio;
using MundoDoCinema.WebApi.Infraestrutura;
using MundoDoCinema.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MundoDoCinema.WebApi.Controllers
{
    [Route("mundodocinema/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly FilmesRepositorio _filmesRepositorio;
        private readonly ILogger<FilmesController> _logger;

        public FilmesController(ILogger<FilmesController> logger, FilmesRepositorio filmesRepositorio)
        {
            _logger = logger;
            _filmesRepositorio = filmesRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync([FromBody] NewFilmeInputModel inputModel, CancellationToken cancellationToken)
        {
            var filme = Filme.Criar(inputModel.Titulo, inputModel.Sinopse, inputModel.Duracao);
            if (filme.IsFailure)
            {
                _logger.LogError(filme.Error);
                return BadRequest(filme.Error);
            }
                
            await _filmesRepositorio.InserirAsync(filme.Value, cancellationToken);
            await _filmesRepositorio.CommitAsync(cancellationToken);

            _logger.LogInformation("Filme {filme} foi criado com sucesso", filme.Value.Id);

            return Ok(filme.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var filme = await _filmesRepositorio.BuscarPorIdAsync(id, cancellationToken);

            _logger.LogInformation("ID do {filme} foi buscado ", id);

            return Ok(filme);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] UpdateFilmeInputModel filmeInputModel, CancellationToken cancellationToken)
        {
            var filme = await _filmesRepositorio.BuscarPorIdAsync(id, cancellationToken);
            if (filme == null)
            {
                var erro = "Filme não encontrado";
                _logger.LogError(erro);
                return NotFound(erro);
            }

            if (string.IsNullOrEmpty(filmeInputModel.Titulo))
            {
                var erro = "Título deve ser informado";
                _logger.LogError(erro);
                return BadRequest(erro);
            }

            filme.Titulo  = filmeInputModel.Titulo;
            filme.Duracao = filmeInputModel.Duracao;
            filme.Sinopse = filmeInputModel.Sinopse;

            _filmesRepositorio.Alterar(filme);
            await _filmesRepositorio.CommitAsync(cancellationToken);

            _logger.LogInformation("Filme {filme} gravado com sucesso", filme.Id);

            return Ok(filme);
        }

        [HttpGet]
        public async Task<IActionResult> ListarFilmes(CancellationToken cancellationToken)
        {
            return Ok(await _filmesRepositorio.ListarFilmesAsync(cancellationToken));
        }

    }
}