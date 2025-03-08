# POCs de Entity Framework Core

Este repositório é um projeto de laboratório criado para estudos e experimentação com o **Entity Framework**, um dos frameworks de ORM (Object-Relational Mapping) mais populares no ecossistema .NET.

O repositório lab_entity_framework_core é usado somenta como base para reaproveitamento nos projetos citados abaixo.

Este repositório contém três projetos distintos que demonstram diferentes abordagens para trabalhar com o Entity Framework Core:

- `lab_entity_framework_core_automatico`
- `lab_entity_framework_core_fluent_api`
- `lab_entity_framework_core_data_annotations`

Cada um desses projetos implementa a persistência de dados de maneiras diferentes, explorando as opções disponíveis no EF Core.

## 📌 Diferença entre os projetos

### 1️⃣ `lab_entity_framework_core_automatico`

Este projeto utiliza a **configuração automática** do Entity Framework Core, onde as convenções padrão do EF são aplicadas sem necessidade de configurações explícitas.

✅ Vantagens:
- Menos código de configuração.
- Mais rápido para começar, ideal para projetos simples.

⚠️ Desvantagens:
- Pouco controle sobre a estrutura do banco de dados.
- Pode gerar nomes de tabelas e colunas que não atendem às necessidades do projeto.

### 2️⃣ `lab_entity_framework_core_fluent_api`

Neste projeto, utilizamos a **Fluent API** para configurar o mapeamento das entidades.

✅ Vantagens:
- Maior controle sobre o esquema do banco de dados.
- Possibilidade de configurar relacionamentos complexos.

⚠️ Desvantagens:
- Exige mais código e conhecimento do EF Core.
- Pode tornar a configuração inicial mais trabalhosa.

### 3️⃣ `lab_entity_framework_core_data_annotations`

Este projeto utiliza **Data Annotations**, que são atributos adicionados diretamente nas classes para definir regras de mapeamento.

✅ Vantagens:
- Configuração mais declarativa e intuitiva.
- Facilita a leitura e manutenção do código.

⚠️ Desvantagens:
- Menos flexibilidade em relação à Fluent API.
- Pode tornar as classes de domínio mais poluídas com atributos de configuração.

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

- **Domain/**: Contém as definições das classes que representam as entidades do banco de dados.
- **Utils/**: Utilizado para implementar o Bogus;
- **Data/**: Inclui o DbContext e configurações relacionadas ao acesso a dados.
- **Migrations/**: Diretório gerado pelo Entity Framework para controle de versão do esquema do banco de dados.
- **Scrtipts/**: Scripts gerados pelo comando dotnet ef migrations script -o scripts\PrimeiraMigracao.SQL
- **Program.cs**: Ponto de entrada da aplicação, onde é feita a configuração inicial.

## Tecnologias Utilizadas

- **C#**
- **Entity Framework Core**
- **.NET**
- **SQL Server** (ou outro banco de dados à sua escolha)
-  **Bogus**

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
1.
   ```bash
   Cria uma nova migração chamada `PrimeiraMigracao`, gerando arquivos dentro da pasta `Migrations`.
   dotnet ef migrations add PrimeiraMigracao  

2.
   ```bash
   Remove a última migração criada ou uma específica, se o nome for informado. Só pode ser usada se a migração ainda não foi aplicada ao banco de dados.
   dotnet ef migrations remove 20250217232714_PrimeiraMigracao  

3.
   ```bash
   Aplica todas as migrações pendentes ao banco de dados.
   dotnet ef database update  

4.
   ```bash
   Lista todas as migrações criadas no projeto.
   dotnet ef migrations list  

5.
   ```bash
   Gera um arquivo SQL contendo o script da migração e o salva no caminho especificado.
   dotnet ef migrations script -o scripts\PrimeiraMigracao.SQL  

6.
   ```bash
   Aplica todas as migrações pendentes ao banco de dados.
   dotnet ef database update  

7.
   ```bash
   Gera arquivo idepotentes com validações para saber se as migrações foram executadas
   dotnet ef migrations script -o Scripts\PrimeiraMigracaoIdepotente.SQL -i

8.
   ```bash
   Sincroniza o banco de dados com a migração que foi informada
   dotnet ef database update 20250218000011_20250217232714_PrimeiraMigracao

## Pacotes para usar o Entity Framework Core com SQL Server:

1.
   ```bash
   Instala o Entity Framework Core no projeto.
   dotnet add package Microsoft.EntityFrameworkCore  

2.
   ```bash
   Instala a ferramenta `dotnet ef` globalmente.
   dotnet tool install --global dotnet-ef  

3.
   ```bash
   Instala o provedor do Entity Framework Core para SQL Server.
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer  

4.
   ```bash
   Instala o pacote necessário para criar e aplicar migrações.
   dotnet add package Microsoft.EntityFrameworkCore.Design  

