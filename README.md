# CRUD-MongoDB-CSharp
Aplicação console em C# utilizando MongoDB para realizar operações CRUD de Autores e Livros.

📚 Exercício: CRUD básico em MongoDB usando C# em Console Application

🎯 Objetivo
Este exercício visa consolidar seu conhecimento sobre operações básicas de banco de dados MongoDB (CRUD: Create, Read, Update, Delete) utilizando a linguagem C# em uma aplicação Console Application.
Além disso, você irá trabalhar com duas collections relacionadas, exercitando a modelagem simples de dados e consultas entre elas.

🧠 Contexto
Você deverá desenvolver uma aplicação que manipule dados sobre autores de livros e os próprios livros.
Para isso, utilize duas collections no MongoDB:
*Authors → Armazena informações dos autores
*Books → Armazena os livros associados a esses autores

✅ Requisitos
01. Configuração
    -Configure e inicialize o banco de dados MongoDB localmente.
    -Utilize o driver oficial do MongoDB para C# para realizar a conexão e operações.

02. Estrutura das Collections

    *Authors
    -Deve conter pelo menos os campos:
    -Id (identificador único gerado automaticamente)
    -Name (nome do autor)
    -Country (país de origem)

    *Books
    -Deve conter pelo menos os campos:
    -Id (identificador único)
    -Title (título do livro)
    -AuthorId (referência ao Id do autor na collection Authors)
    -Year (ano de publicação)

03. Operações CRUD
Desenvolva operações para:
    -Create - Inserir pelo menos um autor e um livro relacionado a esse autor.
    -Read - Listar todos os autores e todos os livros, exibindo juntamente o nome do autor de cada livro (simulando um join).
    -Update - Atualizar informações de um autor (por exemplo, alterar o país).
    -Delete - Remover um livro e um autor da base de dados.

04. Console Application
  A aplicação deve ser desenvolvida para ser executada como um programa de console no .NET.
  Exiba mensagens claras no console para indicar cada etapa e resultado das operações.
  Utilize métodos assíncronos para as operações com o MongoDB.
