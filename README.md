# Entity Framework Lab

Este repositório é um projeto de laboratório criado para estudos e experimentação com o **Entity Framework**, um dos frameworks de ORM (Object-Relational Mapping) mais populares no ecossistema .NET.

## Objetivo

Demonstrar como utilizar o Entity Framework para:
- Criar modelos de dados.
- Configurar mapeamento de entidades para tabelas de banco de dados.
- Realizar operações CRUD (Create, Read, Update, Delete).
- Implementar migrações para gerenciar o esquema do banco de dados.
- Trabalhar com consultas LINQ (Language Integrated Query).

## Público-Alvo

Este projeto é voltado para estudantes e profissionais que desejam aprender ou reforçar seus conhecimentos sobre o Entity Framework no contexto de aplicações C#.

## Estrutura do Projeto

- **Models/**: Contém as definições das classes que representam as entidades do banco de dados.
- **Data/**: Inclui o DbContext e configurações relacionadas ao acesso a dados.
- **Migrations/**: Diretório gerado pelo Entity Framework para controle de versão do esquema do banco de dados.
- **Program.cs**: Ponto de entrada da aplicação, onde é feita a configuração inicial.

## Tecnologias Utilizadas

- **C#**
- **Entity Framework Core**
- **.NET**
- **SQL Server** (ou outro banco de dados à sua escolha)

## Como Usar

1. Clone este repositório:

   ```bash
   git clone https://github.com/romariocamilo/lab_entity_framework_core.git

2. Navegue até o diretório do projeto:
   ```bash
   cd <PATH de onde clonou o repositório>/lab_entity_framework_core
   
3. Restaure os pacotes NuGet:
   ```bash
   dotnet restore
   
4. Execute o projeto:
   ```bash
   dotnet run

## Comandos  Entity Framework Core:

```bash
Cria uma nova migração chamada `PrimeiraMigracao`, gerando arquivos dentro da pasta `Migrations`.
dotnet ef migrations add PrimeiraMigracao  

Remove a última migração criada ou uma específica, se o nome for informado. Só pode ser usada se a migração ainda não foi aplicada ao banco de dados.
dotnet ef migrations remove 20250217232714_PrimeiraMigracao  

Aplica todas as migrações pendentes ao banco de dados.
dotnet ef database update  

Lista todas as migrações criadas no projeto.
dotnet ef migrations list  

Gera um arquivo SQL contendo o script da migração e o salva no caminho especificado.
dotnet ef migrations script -o scripts\PrimeiraMigracao.SQL  


Aplica todas as migrações pendentes ao banco de dados.
dotnet ef database update  

Gera arquivo idepotentes com validações para saber se as migrações foram executadas
dotnet ef migrations script -o Scripts\PrimeiraMigracaoIdepotente.SQL -i

Sincroniza o banco de dados com a migração que foi informada
dotnet ef database update 20250218000011_20250217232714_PrimeiraMigracao

