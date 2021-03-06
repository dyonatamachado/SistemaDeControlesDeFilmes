# Sistema De Controles De Filmes

Este projeto é uma Web Api com basicamente 3 controladores: Filmes, Usuários e Exibições.

Veja abaixo as orientações de uso da Api:

## 1) Filmes

Este Controlador pode ser acessado pela rota Filmes e possui no total 7 controles.

### Post - Rota: /Filmes/
#### Cadastra um Filme no banco de dados

É necessário informar via Body os dados para cadastro de um novo usuário. Os dados necessários são: Titulo, Genero, Sinopse e Ano. Todos os dados são strings exceto o ano que é de formato numérico. Todos os dados são obrigatórios. A Api avalia se já existe algum filme cadastrado com o mesmo Titulo e Ano se sim retornará o Status Code 400. Caso não exista, irá cadastrar o novo filme e retornará Status Code 201 informando a rota para acessar o recurso criado. 

### Put - Rota: /Filmes/{id}
#### Atualiza dados de um Filme já cadastrado e ativo

Para acessar este controlador é necessário informar pela rota, o Id do filme que deseja atualizar. E é necessário informar via Body os dados de Titulo, Genero, Sinopse e Ano conforme o padrão. A Api verifica se existe filme com o Id informado, caso não exista retorna o Código 404. Caso exista, a Api verifica se os dados informados na requisição estão vinculados à algum filme no Banco de Dados e em caso positivo compara com o Id informado e havendo inconsistência retorna o Código 400. A Api ainda verifica se o filme com o Id informado está ativo no BD. Em caso de filme cadastrado mas inativo, retorna 400 informando a necessidade de reativar o filme. Se nenhum destes se aplicar, a API então atualiza o recurso no BD e retorna 204.

### Get - Rota: /Filmes/
#### Retorna todos os filmes cadastrados e ativos

Esta retorna todos os filmes cadastrados e ativos. Não é necessário informar nenhum parâmetro. Em caso de não haver nenhum filme cadastro ou ativo retorna 204 e caso haja retorna 200 com os filmes encontrados. Os filmes são retornados com uma ViewModel que inclui a quantidade de espectadores além dos dados de cadastro do filme. Caso houver filmes cadastrados porém inativos, há uma rota específica para acessá-los.

### Get - Rota: /Filmes/{id}
#### Retorna o filme cadastrado com o Id informado

Retorna o filme cadastrado com o Id informado pela rota. Mas retorna apenas se o filme estiver ativo. No caso em que o filme não esteja cadastrado ou esteja inativo a Api retorna o código 404 e caso o recurso seja encontrado retorna 200.

### Get - Rota: /Filmes/inativos
#### Retorna filmes cadastrados mas inativos

Retorna todos os filmes cadastrados mas inativos com status 200. Mas se não houver filme inativo retorna 204.

### Delete - Rota: /Filmes/{id}
#### Desativa o filme com o Id informado

Desativa o filme com o Id informado pela rota. Se o filme informado estiver cadastrado mas já estiver inativo retorna 400. Caso não estiver cadastrado retorna 404. Caso seja encontrado e estiver ativo, a Api faz um SoftDelete e o marca como Inativo e retorna 204.

### Patch - Rota: /Filmes/{id}
#### Reativa o filme com o Id informado

Reativa o filme com o Id informado pela rota. Se o filme informado estiver cadastrado mas já estiver ativo retorna 400. Caso não estiver cadastrado retorna 404. Caso seja encontrado e estiver inativo, a Api remove a marcação de Inativo e retorna 204.

## 2) Usuários

Este Controlador pode ser acessado pela rota Usuarios e possui no total 7 controles.

### Post - Rota: /Usuarios/
#### Cadastra um Usuário no banco de dados

É necessário informar via Body os dados para cadastro de um novo usuário. Os dados necessários são: Nome, CPF e Data de Nascimento. Todos são strings exceto o Data que é formato DateTime. Todos os dados são obrigatórios. A Api avalia se já existe algum usuário cadastrado com o mesmo CPF, se sim retornará o Status Code 400. Caso não exista, irá cadastrar o novo usuário e retornará Status Code 201 informando a rota para acessar o recurso criado. 

### Put - Rota: /Usuarios/{id}
#### Atualiza dados de um usuário já cadastrado e ativo

Para acessar este controlador é necessário informar pela rota, o Id do usuário que deseja atualizar. E é necessário informar via Body os dados Nome, CPF e Data de Nascimento conforme o mesmo padrão de cadastro. A Api verifica se existe usuário com o Id informado, caso não exista retorna o Código 404. Caso exista, a Api verifica se os dados informados na requisição estão vinculados à algum usuário no Banco de Dados e em caso positivo compara com o Id informado e havendo inconsistência retorna o Código 400. A Api ainda verifica se o usuário com o Id informado está ativo no BD. Em caso de usuário cadastrado mas inativo, retorna 400 informando a necessidade de reativar o usuário. Se nenhum destes se aplicar, a API então atualiza o recurso no BD e retorna 204.

### Get - Rota: /Usuarios/
#### Retorna todos os usuários cadastrados e ativos

Esta retorna todos os usuários cadastrados e ativos. Não é necessário informar nenhum parâmetro. Em caso de não haver nenhum usuário cadastrado ou ativo retorna 204 e caso haja retorna 200 com os usuários encontrados. Os usuários são retornados com uma ViewModel que inclui a quantidade de filmes assistidos além dos dados de cadastro do usuário. Caso houver usuários cadastrados porém inativos, há uma rota específica para acessá-los.

### Get - Rota: /Usuarios/{id}
#### Retorna o usuário cadastrado com o Id informado

Retorna o usuário cadastrado com o Id informado pela rota. Mas retorna apenas se o usuário estiver ativo. No caso em que o usuário não esteja cadastrado ou esteja inativo a Api retorna o código 404 e caso o recurso seja encontrado retorna 200.

### Get - Rota: /Usuarios/inativos
#### Retorna usuários cadastrados mas inativos

Retorna todos os usuários cadastrados mas inativos com status 200. Mas se não houver usuário inativo retorna 204.

### Delete - Rota: /Usuarios/{id}
#### Desativa o usuário com o Id informado

Desativa o usuário com o Id informado pela rota. Se o usuário informado estiver cadastrado mas já estiver inativo retorna 400. Caso não estiver cadastrado retorna 404. Caso seja encontrado e estiver ativo, a Api faz um SoftDelete e o marca como Inativo e retorna 204.

### Patch - Rota: /Usuarios/{id}
#### Reativa o usuário com o Id informado

Reativa o usuário com o Id informado pela rota. Se o usuário informado estiver cadastrado mas já estiver ativo retorna 400. Caso não estiver cadastrado retorna 404. Caso seja encontrado e estiver inativo, a Api remove a marcação de Inativo e retorna 204.

## 3) Exibições

Este Controlador é responsável pelo histórico de filmes assistidos por cada usuário. Pode ser acessado pela rota Exibicoes e tem 6 controles.

### Post - Rota: /Exibicoes/
#### Cria uma nova Exibição

É necessário informar via Body os dados de FilmeId e UsuarioId ambos no formato numérico. Caso o filme ou o usuário não for encontrado a API retornará código 400. Se o filme já estiver no histórico de visualizações do usuário a API retornará 400. Em caso contrário, a API registrará a exibição e retornará 201 com a rota para acessar o recurso.

### Delete - Rota: /Exibicoes/{id}
#### Apaga o registro de exibição

Apaga o registro de exibição com o Id informado via rota. A Api retorna 404 caso a exibição não seja localizada ou 204 caso encontrada. Neste caso o recurso de fato é apagado do banco de dados. 

### Get - Rota: /Exibicoes/
#### Retorna todas as exibições cadastradas

A API pode retornar 204 caso não haja nenhuma exibição cadastrada ou 200 caso haja e então retorna a lista de exibições.

### Get - Rota: /Exibicoes/{id}
#### Retorna a exibição com o Id especificado

A API pode retornar 404 caso não haja nenhuma exibição com este ID ou 200 caso haja e então lista os dados da exibição.

### Get - Rota: /Exibicoes/UsuarioId/{usuarioId}
#### Retorna todas as exibições cadastradas para o usuário informado

A API pode retornar 404 caso não haja nenhuma exibição cadastrada para este usuário. Pode retonar 200 com a lista de exibições deste usuário.

### Get - Rota: /Exibicoes/FilmeId/{filmeId}
#### Retorna todas as exibições cadastradas para o filme informado

A API pode retornar 404 caso não haja nenhuma exibição cadastrada para o filme informado. Pode retonar 200 com a lista de exibições deste filme.









