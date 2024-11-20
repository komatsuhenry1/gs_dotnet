# Smart Energy

### integrantes da equipe:

|Rm|Nome|Turma
|--|--|--|
RM99503|Arthur Koga|TDSA
RM552254|Gabriel Benjamim|TDSA
RM99476|Luiz Felipe|TDSA
RM550684|Henry Ribeiro|TDSR
RM99538|Murilo José|TDSR

Global Solution .Net






## Endpoints

- __Client__
    - [Listar cliente](#listar-cliente)
    - [Detalhar](#detalhar-cliente)
    - [Cadastrar](#cadastrar-cliente)
    - [Apagar](#apagar-cliente)

- __Swagger:__
  http://localhost:52941/swagger/index.html

---

## Client

### Listar Cliente
`GET` /cpf/client

__descrição:__ Retornar um array com todos os clientes cadastrados.

**Exemplo de Resposta**
```js
[
    {
        "id": 1,
        "name": "Murilo",
        "sobreNome": "Sousa",
        "email": "muriloSousa@gmail.com",
        "idade": 23
    }
]
```

**Códigos de Status**

| Código | Descrição|
|--------|----------|
|200|Dados retornados com sucesso

---

### Detalhar Cliente 

`GET` /cpf/client/{id}

__descrição:__ Retornar os dados do cliente com o `id` informado.

**Exemplo de Resposta**
```js
    {
        "id": 1,
        "name": "Murilo",
        "sobreNome": "Sousa",
        "email": "muriloSousa@gmail.com",
        "idade": 23
    }
```

**Códigos de Status**

| Código | Descrição|
|--------|----------|
|200|Dados retornados com sucesso
|404|Cliente não encontrado 

---

### Cadastrar Cliente

`POST` /cpf/client

__descrição:__ Insere uma novo cliente

**Corpo da requisição**
|Campo|Tipo|Obrigatório|Descrição
|:-----:|:----:|:-----------:|---------
|name|string| ✅ |Nome do cliente
|sobreNome |string| ✅ |sobreNome do cliente
|email|string| ✅ |email do cliente
|idade|Integer| ✅ |idade do cliente

```js
    {
        "name": "Murilo",
        "sobreNome": "Sousa",
        "email": "muriloSousa@gmail.com",
        "idade": 23
    }
```
**Exemplo de Resposta**

```js
    {
        "id": 1,
        "name": "Murilo",
        "sobreNome": "Sousa",
        "email": "muriloSousa@gmail.com",
        "idade": 23
    }
```

**Códigos de Status**
| Código | Descrição|
|--------|----------|
|200|Dados retornados com sucesso
|400|Erro de validação - verifique o corpo da requisição
---

### Apagar Cliente
`DELETE` /cpf/client/{id}

__descrição:__ Apagar o Cliente com o `id` indicado

**Códigos de Status**

| Código | Descrição                   |
|--------|-----------------------------|
| 204    | Cliente apagado com sucesso 
| 404    | Cliente não encontrado      

--- 

### Editar cliente

`PUT` /cpf/client/{id}

Atualizar os dados do `id` indicado

**Corpo da requisição**

| Campo |  Tipo  | Obrigatório |Descrição
|:-----:|:------:|:-----------:|---------
| name  | string |      ❌      |Nome do cliente 
|sobreNome |string| ❌ |sobreNome do cliente
|email|string| ❌ |email do cliente
|idade|Integer| ❌ |idade do cliente

```js
    {
        "name": "Martins",
        "sobreNome": "josé",
        "email": "martins@gmail.com",
        "idade": 21
    }
```

**Exemplo de Resposta**

```js
{   
    "id": 1,
    "name": "Martins",
    "sobreNome": "josé",
    "email": "martins@gmail.com",
    "idade": 21
}
```

**Códigos de Status**

| Código | Descrição                                            |
|--------|------------------------------------------------------|
| 200    | Cliente atualizado com sucesso                       
| 400    | A validação falhou - verifique o corpo da requisição 
| 404    | Cliente não encontrado                               

---