using System;
using CSharpFunctionalExtensions;

namespace MundoDoCinema.WebApi.Dominio
{
    public sealed class Filme
    {
        public Filme(Guid id, string titulo, string sinopse, int duracao)
        {
            Id      = id;
            Titulo  = titulo;
            Duracao = duracao;
            Sinopse = sinopse;
        }

        public Guid Id { get; }

        public string Titulo { set; get; }

        public int Duracao { set; get; }

        public string Sinopse { set; get; }

        public static Result<Filme> Criar(string titulo, string sinopse, int duracao)
        {
            if (string.IsNullOrEmpty(titulo))
                return Result.Failure<Filme>("Deve informar o título");

            return new Filme(Guid.NewGuid(), titulo, sinopse, duracao);
        }
    }
}