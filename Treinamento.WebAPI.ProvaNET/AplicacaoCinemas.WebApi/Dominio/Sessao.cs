using System;
using CSharpFunctionalExtensions;
using System.Security.Cryptography;
using System.Text;

namespace MundoDoCinema.WebApi.Dominio
{
    public sealed class Sessao
    {
        private string _hashConcorrencia;
        public Sessao(Guid id, DateTime dia, int inicioHora, int inicioMinuto, int quantidadeLugares, 
                      double preco, Guid idDoFilme, string hashConcorrencia)
        {
            Id                = id;
            Dia               = dia;
            Preco             = preco;
            InicioHora        = inicioHora;
            InicioMinuto      = inicioMinuto;
            QuantidadeLugares = quantidadeLugares;
            QuantidadeLivres  = quantidadeLugares;            
            IdDoFilme         = idDoFilme;
            _hashConcorrencia = hashConcorrencia;
        }

        public Guid Id { get; } 

        public DateTime Dia { set; get; }

        public double Preco { set; get; }

        public int InicioHora { set; get; }

        public int InicioMinuto { set; get; }

        public int QuantidadeLugares { set; get; }

        public int QuantidadeLivres { set; get; }
        
        public Guid IdDoFilme { set; get; }


        public static Result<Sessao > Criar(DateTime dia, int inicioHora, int inicioMinuto, int quantidadeLugares, double preco, Guid idDoFilme)
        {
            if (idDoFilme.ToString() == null)
                return Result.Failure<Sessao>("Deve informar o filme da sessao");

            if(preco <= 0)
                return Result.Failure<Sessao>("Valor inválido");

            if (quantidadeLugares <= 0)
                return Result.Failure<Sessao>("Valor inválido");            

            if (inicioHora < 0 | inicioHora > 23)
                return Result.Failure<Sessao>("Valor inválido");

            if (inicioMinuto < 0 | inicioMinuto > 59)
                return Result.Failure<Sessao>("Valor inválido");

            var sessao = new Sessao(Guid.NewGuid(), dia, inicioHora, inicioMinuto, quantidadeLugares, preco, idDoFilme, "");

            sessao.AtualizarHashConcorrencia();
            return sessao;
        }

        public void AtualizarHashConcorrencia()
        {
            using var hash = MD5.Create();
            var data = hash.ComputeHash(
                Encoding.UTF8.GetBytes(
                    $"{Id}{Dia}{InicioHora}{QuantidadeLugares}{QuantidadeLivres}{Preco}{IdDoFilme}"));
            var sBuilder = new StringBuilder();
            foreach (var tbyte in data)
                sBuilder.Append(tbyte.ToString("x2"));
            _hashConcorrencia = sBuilder.ToString();
        }
    }
}