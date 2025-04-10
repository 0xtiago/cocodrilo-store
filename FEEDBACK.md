# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Possui views e rotas definidas para as funcionalidades no MVC
    - Interface web implementada com Razor Pages/Views

### Design
 - Será avaliado na entrega final

### Funcionalidade
  * Pontos positivos:
    - Implementação de CRUD para Produtos e Categorias

## Back End
### Arquitetura
  * Pontos positivos:
    - Estrutura com projetos MVC e API separados
    - Uso do Entity Framework Core para acesso a dados

  * Pontos negativos:
    - Falta de uma camada core para compartilhar lógica entre MVC e API
    - Possível duplicação de código entre os projetos MVC e API
    - Dependencia (acoplamento) entre MVC e API

### Funcionalidade
  * Pontos positivos:
    - Implementação de autenticação com ASP.NET Core Identity
    - Uso do EF com SQLite conforme documentado
    - API RESTful implementada com endpoints documentados

### Modelagem
  * Pontos positivos:
    - Modelagem simples utilizando Entity Framework Core
    - Uso de SQLite como banco de dados

## Projeto
### Organização
  * Pontos positivos:
    - Possui arquivo solution (.sln) na raiz

  * Pontos negativos:
    - Muitos arquivos e pastas desnecessárias na raiz

### Documentação
  * Pontos positivos:
    - README.md bem detalhado com instruções de instalação e execução
    - Documentação da API com Swagger implementada
    - Instruções claras para execução do projeto

  * Pontos negativos:
    - Nenhum ponto negativo significativo identificado na documentação

### Instalação
  * Pontos positivos:
    - Implementação do SQLite como banco de dados
    - Scripts de start e stop para automatizar a execução
    - Instruções detalhadas de instalação no README

  * Pontos negativos:
    - Não há evidências claras sobre a implementação de seeds de dados e Migration automatica