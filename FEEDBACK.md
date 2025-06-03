# Feedback - Avaliação Geral

## Front End

### Navegação
  * Pontos positivos:
    - Projeto Web (MVC) funcional, com rotas e views para categorias, produtos e autenticação.
    - Layout estruturado e páginas acessíveis conforme as funcionalidades.

  * Pontos negativos:
    - Há acoplamento indevido entre os projetos MVC e API, o que viola a independência exigida entre as camadas.

### Design
  - Design da aplicação simples e funcional, suficiente para o contexto administrativo.

### Funcionalidade
  * Pontos positivos:
    - Implementações básicas de CRUD funcionais em ambas as camadas (API e MVC).
    - Autenticação com ASP.NET Identity presente.
    - Uso de SQLite, migrations e seed de dados implementados corretamente.

  * Pontos negativos:
    - A entidade `Vendedor` não é criada junto ao usuário no registro, contrariando a especificação de compartilhamento de ID.
    - Falta de camada Core para centralizar a lógica de negócio, o que pode gerar duplicação.

## Back End

### Arquitetura
  * Pontos positivos:
    - Projeto separado entre API e Web, com estrutura básica funcional.
    - Camada de apresentação clara.

  * Pontos negativos:
    - Ausência de uma camada "Core" ou "Shared" para compartilhamento de regras e abstrações entre API e MVC.
    - Potencial duplicação de código entre os projetos.
    - Acoplamento entre MVC e API observável em chamadas diretas e dependências.

### Funcionalidade
  * Pontos positivos:
    - CRUD de produtos e categorias com operações básicas atendidas.
    - Autenticação operante com login e proteção de rotas.
  
  * Pontos negativos:
    - Ausência da criação do vendedor no processo de registro do usuário.

### Modelagem
  * Pontos positivos:
    - Entidades simples e adequadas à proposta do domínio.
  
  * Pontos negativos:
    - A modelagem está vinculada apenas ao uso local de cada camada (sem camada de compartilhamento entre API e MVC).

## Projeto

### Organização
  * Pontos positivos:
    - Uso de `src`, solution na raiz, arquivos `README.md` e `FEEDBACK.md` presentes.
    - Separação de projetos funcional.

  * Pontos negativos:
    - Armazenamento e versionamento de arquivos de banco SQLite no repositório, o que é inadequado considerando o uso de seed de dados.

### Documentação
  * Pontos positivos:
    - Presença de README, FEEDBACK e imagens ilustrativas.
    - Swagger implementado na API.

### Instalação
  
  * Pontos negativos:
    - Armazenamento do arquivo `.db` é inadequado, deve-se usar apenas scripts/seeds.
    - Seed e migrations automáticas não implementados.

---

# 📊 Matriz de Avaliação de Projetos

| **Critério**                   | **Peso** | **Nota** | **Resultado Ponderado**                  |
|-------------------------------|----------|----------|------------------------------------------|
| **Funcionalidade**            | 30%      | 6        | 1,8                                      |
| **Qualidade do Código**       | 20%      | 8        | 1,6                                      |
| **Eficiência e Desempenho**   | 20%      | 6        | 1,2                                      |
| **Inovação e Diferenciais**   | 10%      | 7        | 0,7                                      |
| **Documentação e Organização**| 10%      | 10       | 1,0                                      |
| **Resolução de Feedbacks**    | 10%      | 7        | 0,7                                      |
| **Total**                     | 100%     | -        | **7,0**                                  |

## 🎯 **Nota Final: 7 / 10**
