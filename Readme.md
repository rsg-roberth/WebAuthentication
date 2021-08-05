# WebAuthentication
Simulação de autenticação de usuário, validação de token e senhas.

Este projeto tem como propósito exercitar:
- Processo de autenticação utilizado o JWT:
    - Validar se o usuário foi ou não autenticado e se for, retornar os seus respectivos dados e o token com um tempo válido (no caso 5m).
    - Permitir consumo de determinados endpoints somente se o usuário estiver autenticado e o token ativo.

- Validação de senhas através de expressões regulares atendendo os requisitos mínimos:
    - Conter no mínimo 15 caracteres.
    - No mínimo uma letra maiúscula.
    - No mínimo uma letra minúscula.
    - No mínimo um dos seguintes caracteres especiais: (@,#,_,- e !).
    - Não poder ter caracteres repetidos em sequência, por exemplo: 1111,aaaa, bbbb, @@@@, BBBB.
    - Prever case-sensitive, por exemplo: A é diferente de a.

- Criação de uma senha que atenda os requisítos anteriores.

O projeto foi desenvolvido em NetCore 5.0 e utilizando algumas bibliotecas do NuGet e possui duas camadas sendo uma de API e outra para testes unitários.

Formas de rodar o projeto: diretamente pelo Kestrel ou através do docker, considerando o arquivo dockerfile embarcado no projeto.

- Ordem de execução:
	- Usuário válido: consumir o endpoint padrão utilizando o verbo POST e passando no body da requisição os dados: login e senha, respectivamente com os dados Admin e !#123@456#!. Este usuário está inserido em uma lista que está implementada dentro de um serviço e este, esta sendo carregado via injeção de dependência.
    - Validação da senha: consumir o endpoint padrão utilizando o verbo GET, passando na url a senha que deseja validar e passando o token obtido na consulta anterior no header da requisição.
    - Criar senha válida: consumir o endpoint padrão utilizando o verbo GET e passando o token obtido na consulta anterior no header da requisição.
