
drop database if exists MundoDoCinema;
create database MundoDoCinema;
 
drop table if exists [MundoDoCinema].[dbo].[Sessoes];
CREATE TABLE [MundoDoCinema].[dbo].[Sessoes] (
    [Id]                      UNIQUEIDENTIFIER NOT NULL,
    [Dia]                     DATE             NOT NULL,
    [Preco]                   FLOAT (53)       NOT NULL,
    [InicioHora]              INT              NOT NULL,
    [InicioMinuto]            INT              NOT NULL,
    [QuantidadeLugares]       INT              NOT NULL,
    [QuantidadeLivres]        INT              NOT NULL,    
    [IdDoFilme]               UNIQUEIDENTIFIER NOT NULL,
    [token_concorrencia]      VARCHAR (MAX)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sessoes_ToTable] FOREIGN KEY ([IdDoFilme]) REFERENCES [dbo].[Filmes] ([Id])
);

drop table if exists [MundoDoCinema].[dbo].[Filmes];
CREATE TABLE [MundoDoCinema].[dbo].[Filmes] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Titulo]        VARCHAR (30)     NOT NULL,
    [Duracao]       INT              NOT NULL,
    [Sinopse]       VARCHAR (500)    NOT NULL,    
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

