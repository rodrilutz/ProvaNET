using System;
using System.Collections.Generic;
using System.Linq;
using MundoDoCinema.WebApi.Dominio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MundoDoCinema.WebApi.Infraestrutura
{
    public sealed class SessoesRepositorio
    {
        private readonly MundoDoCinemaDbContext _dbContext;

        public SessoesRepositorio(MundoDoCinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Sessao novaSessao, CancellationToken cancellationToken = default)
        {
            await _dbContext.Sessoes.AddAsync(novaSessao, cancellationToken);
        }
        public void Alterar(Sessao sessao)
        {
         
        }

        public async Task <Sessao> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                  .Sessoes
                  .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<Sessao>> ListarSessoesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext
                         .Sessoes
                         .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<Sessao>> BuscarParaFilmeDiaAsync(Guid IdDoFilme, DateTime dia, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                         .Sessoes
                         .Where(c => c.IdDoFilme == IdDoFilme && c.Dia == dia)
                         .ToListAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}