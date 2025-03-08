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

CREATE TABLE [Pessoa] (
    [Id] int NOT NULL IDENTITY,
    [Nome] VARCHAR(80) NOT NULL,
    [Sobrenome] VARCHAR(80) NOT NULL,
    [Cpf] CHAR(14) NOT NULL,
    [EnderecoId] int NOT NULL,
    [TipoPessoa] int NOT NULL,
    CONSTRAINT [PK_Pessoa] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pessoa_Endereco_EnderecoId] FOREIGN KEY ([EnderecoId]) REFERENCES [Endereco] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Cidade_EstadoId] ON [Cidade] ([EstadoId]);

CREATE INDEX [IX_Endereco_CidadeId] ON [Endereco] ([CidadeId]);

CREATE INDEX [idx_pessoa_cpf] ON [Pessoa] ([Cpf]);

CREATE INDEX [IX_Pessoa_EnderecoId] ON [Pessoa] ([EnderecoId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250228010328_PrimeiraMigracao', N'9.0.2');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Endereco]') AND [c].[name] = N'Numero');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Endereco] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Endereco] ALTER COLUMN [Numero] VARCHAR(10) NOT NULL;

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Endereco]') AND [c].[name] = N'Nome');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Endereco] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Endereco] ALTER COLUMN [Nome] VARCHAR(80) NOT NULL;

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Endereco]') AND [c].[name] = N'Logradouro');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Endereco] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Endereco] ALTER COLUMN [Logradouro] VARCHAR(80) NOT NULL;

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Endereco]') AND [c].[name] = N'Complemento');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Endereco] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Endereco] ALTER COLUMN [Complemento] VARCHAR(80) NOT NULL;

CREATE INDEX [idx_endereco_id] ON [Endereco] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250308132758_AddContextEndereco', N'9.0.2');

DROP INDEX [idx_endereco_id] ON [Endereco];

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estado]') AND [c].[name] = N'Nome');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Estado] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Estado] ALTER COLUMN [Nome] VARCHAR(80) NOT NULL;

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Cidade]') AND [c].[name] = N'Nome');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Cidade] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Cidade] ALTER COLUMN [Nome] VARCHAR(80) NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250308133250_AddContextCidadeEstado', N'9.0.2');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250308140640_TesteMiogration', N'9.0.2');

COMMIT;
GO

