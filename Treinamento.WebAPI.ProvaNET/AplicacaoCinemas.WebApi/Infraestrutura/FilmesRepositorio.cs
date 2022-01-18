using System;
using System.Collections.Generic;
using System.Linq;
using MundoDoCinema.WebApi.Dominio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MundoDoCinema.WebApi.Infraestrutura
{
    public sealed class FilmesRepositorio
    {
        private readonly MundoDoCinemaDbContext _dbContext;

        public FilmesRepositorio(MundoDoCinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Filme novoFilme, CancellationToken cancellationToken = default)
        {
            await _dbContext.Filmes.AddAsync(novoFilme, cancellationToken);
        }
        public void Alterar(Filme filme)
        {
            
        }

        public async Task<Filme> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                         .Filmes
                         .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<Filme>> ListarFilmesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext
                         .Filmes
                         .ToListAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}