IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Estado] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [NomeCidade] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Estado] PRIMARY KEY ([Id])
);

CREATE TABLE [Cidade] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [EstadoId] int NOT NULL,
    CONSTRAINT [PK_Cidade] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cidade_Estado_EstadoId] FOREIGN KEY ([EstadoId]) REFERENCES [Estado] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Endereco] (
    [Id] int NOT NULL IDENTITY,
    [Logradouro] nvarchar(max) NOT NULL,
    [Nome] nvarchar(max) NOT NULL,
    [Numero] nvarchar(max) NOT NULL,
    [Complemento] nvarchar(max) NOT NULL,
    [CidadeId] int NOT NULL,
    CONSTRAINT [PK_Endereco] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Endereco_Cidade_CidadeId] FOREIGN KEY ([CidadeId]) REFERENCES [Cidade] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Tabela_Pessoa] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Sobrenome] nvarchar(50) NOT NULL,
    [Cpf_Pessoa] nvarchar(max) NOT NULL,
    [EnderecoId] int NOT NULL,
    [TipoPessoa] int NOT NULL,
    CONSTRAINT [PK_Tabela_Pessoa] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tabela_Pessoa_Endereco_EnderecoId] FOREIGN KEY ([EnderecoId]) REFERENCES [Endereco] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Cidade_EstadoId] ON [Cidade] ([EstadoId]);

CREATE INDEX [IX_Endereco_CidadeId] ON [Endereco] ([CidadeId]);

CREATE INDEX [IX_Tabela_Pessoa_EnderecoId] ON [Tabela_Pessoa] ([EnderecoId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250308185637_PrimeiraMigracao', N'9.0.2');

COMMIT;
GO

