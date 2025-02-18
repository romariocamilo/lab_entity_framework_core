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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    CREATE TABLE [Estado] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Estado] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    CREATE TABLE [Cidade] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [EstadoId] int NOT NULL,
        CONSTRAINT [PK_Cidade] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Cidade_Estado_EstadoId] FOREIGN KEY ([EstadoId]) REFERENCES [Estado] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    CREATE TABLE [Endereco] (
        [Id] int NOT NULL IDENTITY,
        [Logradouro] nvarchar(max) NOT NULL,
        [Nome] nvarchar(max) NOT NULL,
        [Numero] int NOT NULL,
        [Complemento] nvarchar(max) NOT NULL,
        [CidadeId] int NOT NULL,
        CONSTRAINT [PK_Endereco] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Endereco_Cidade_CidadeId] FOREIGN KEY ([CidadeId]) REFERENCES [Cidade] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    CREATE TABLE [Pessoas] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Sobrenome] nvarchar(max) NOT NULL,
        [Cpf] nvarchar(max) NOT NULL,
        [EnderecoId] int NOT NULL,
        [TipoPessoa] int NOT NULL,
        CONSTRAINT [PK_Pessoas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pessoas_Endereco_EnderecoId] FOREIGN KEY ([EnderecoId]) REFERENCES [Endereco] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    CREATE INDEX [IX_Cidade_EstadoId] ON [Cidade] ([EstadoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    CREATE INDEX [IX_Endereco_CidadeId] ON [Endereco] ([CidadeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    CREATE INDEX [IX_Pessoas_EnderecoId] ON [Pessoas] ([EnderecoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250218000011_20250217232714_PrimeiraMigracao'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250218000011_20250217232714_PrimeiraMigracao', N'9.0.2');
END;

COMMIT;
GO

