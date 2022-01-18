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
    public class SessoesController : ControllerBase
    {
        private readonly SessoesRepositorio _sessoesRepositorio;
        private readonly FilmesRepositorio _filmesRepositorio;
        private readonly ILogger<SessoesController> _logger;

        public SessoesController(SessoesRepositorio sessoesRepositorio, FilmesRepositorio filmesRepositorio,
                                 ILogger<SessoesController> logger)
        {
            _logger = logger;
            _sessoesRepositorio = sessoesRepositorio;
            _filmesRepositorio = filmesRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync([FromBody] NewSessaoInputModel inputModel, CancellationToken cancellationToken)
        {
            var sessao = Sessao.Criar(inputModel.Dia, inputModel.InicioHora, inputModel.InicioMinuto,
                inputModel.QuantidadeLugares, inputModel.Preco, inputModel.IdDoFilme);
            if (sessao.IsFailure)
            {
                _logger.LogError(sessao.Error);
                return BadRequest(sessao.Error);
            }
                
            var filme = await _filmesRepositorio.BuscarPorIdAsync(inputModel.IdDoFilme, cancellationToken);
            if (filme == null)
            {
                var erro = "Filme não encontrado";
                _logger.LogError(erro);
                return BadRequest(erro);
            }
                
            await _sessoesRepositorio.InserirAsync(sessao.Value, cancellationToken);
            await _sessoesRepositorio.CommitAsync(cancellationToken);

            _logger.LogInformation("Sessão {sessao} foi criada com sucesso", sessao.Value.Id);

            return Ok(sessao.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarAsync(Guid id,[FromBody] UpdateSessaoInputModel inputModel, CancellationToken cancellationToken)
        {
            var sessao = await _sessoesRepositorio.BuscarPorIdAsync(id, cancellationToken);
            if (sessao == null)
            {
                var erro = "Sessão não encontrado";
                _logger.LogError(erro);
                return BadRequest(erro);
            }

            if (inputModel.IdDoFilme.ToString() == null)
            {
                var erro = "Deve ser informado o filme da sessão";
                _logger.LogError(erro);
                return BadRequest(erro);
            }

            if (inputModel.Preco <= 0)
            {
                var erro = "Valor inválido";
                _logger.LogError(erro);
                return BadRequest(erro);
            }

            var filme = await _filmesRepositorio.BuscarPorIdAsync(inputModel.IdDoFilme, cancellationToken);
            if (filme == null)
            {
                var erro = "Filme não encontrado";
                _logger.LogError(erro);
                return BadRequest(erro);
            }
                
            if (inputModel.QuantidadeLugares <= 0)
            {
                var erro = "Valor inválido";
                _logger.LogError(erro);
                return BadRequest(erro);
            }
                
            if (inputModel.InicioHora < 0 | inputModel.InicioHora > 23)
            {
                var erro = "Valor inválido";
                _logger.LogError(erro);
                return BadRequest(erro);
            }
                
            if (inputModel.InicioMinuto < 0 | inputModel.InicioMinuto > 59)
            {
                var erro = "Valor inválido";
                _logger.LogError(erro);
                return BadRequest(erro);
            }
                

            sessao.Dia          = inputModel.Dia;
            sessao.Preco        = inputModel.Preco;
            sessao.InicioHora   = inputModel.InicioHora;
            sessao.IdDoFilme    = inputModel.IdDoFilme;            
            sessao.InicioMinuto = inputModel.InicioMinuto;

            sessao.QuantidadeLivres = inputModel.QuantidadeLugares - (sessao.QuantidadeLugares - sessao.QuantidadeLivres);
            if (sessao.QuantidadeLivres < 0)
            {
                var erro = "Todos os ingressos já foram vendidos";
                _logger.LogError(erro);
                return BadRequest(erro);
            }
                
            sessao.QuantidadeLugares = inputModel.QuantidadeLugares;
            sessao.AtualizarHashConcorrencia();
            
            _sessoesRepositorio.Alterar(sessao);
            await _sessoesRepositorio.CommitAsync(cancellationToken);

            _logger.LogInformation("Sessão {sessao} gravado com sucesso", sessao.Id);

            return Ok(sessao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var sessao = await _sessoesRepositorio.BuscarPorIdAsync(id, cancellationToken);

            _logger.LogInformation("ID da {sessao} foi buscado ",id);

            return Ok(sessao);
        }

        [HttpGet]
        public async Task<IActionResult> ListarSessoes(CancellationToken cancellationToken)
        {
            return Ok(await _sessoesRepositorio.ListarSessoesAsync(cancellationToken));
        }

        [HttpPost("adquiriringresso/{id},{quantidade}")]
        public async Task<IActionResult> AdquirirIngresso(Guid id, int quantidade, CancellationToken cancellationToken)
        {
            var sessao = await _sessoesRepositorio.BuscarPorIdAsync(id, cancellationToken);
            if (sessao == null)
            {
                var erro = "Sessão não encontrada";
                _logger.LogError(erro);
                return BadRequest(erro);
            }
                
            sessao.QuantidadeLivres -= quantidade;
            if (sessao.QuantidadeLivres < 0)
            {
                var erro = "Ingressos esgotados";
                _logger.LogError(erro);
                return BadRequest(erro);
            }

            sessao.AtualizarHashConcorrencia();

            _sessoesRepositorio.Alterar(sessao);
            await _sessoesRepositorio.CommitAsync(cancellationToken);

            _logger.LogInformation("{quantidade} total de ingressos vendidos para a sessão {sessao} ", quantidade, sessao.Id);

            return Ok(sessao);
        }

    }
}