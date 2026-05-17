# DoseCerta CLI

Aplicação CLI desenvolvida em C# para auxiliar no controle de medicamentos, horários de medicação, hidratação e autocuidado.

---

# Problema Real

Muitas pessoas possuem dificuldades em manter regularidade no uso de medicamentos, especialmente em tratamentos contínuos ou com múltiplos horários diários.

O esquecimento da medicação pode comprometer tratamentos, agravar doenças e reduzir a qualidade de vida, principalmente entre idosos, pessoas com rotina intensa e pacientes em acompanhamento médico contínuo.

---

# Proposta da Solução

O DoseCerta CLI busca amenizar esse problema oferecendo uma aplicação simples em linha de comando para:

- cadastrar medicamentos;
- controlar horários;
- identificar medicamentos pendentes;
- marcar medicamentos como tomados;
- acompanhar hidratação e autocuidado.

---

# Público-Alvo

- idosos;
- cuidadores;
- pacientes em tratamento contínuo;
- pessoas com rotina corrida;
- usuários que desejam melhorar organização de autocuidado.

---

# Funcionalidades

## Medicamentos
- cadastro de medicamentos;
- listagem;
- marcação de horários tomados;
- verificação de medicamentos pendentes.

## Hidratação
- registro de consumo de água.

## Autocuidado
- checklist simples de hábitos saudáveis.

---

# Tecnologias Utilizadas

- C#
- .NET 8
- xUnit
- System.Text.Json
- GitHub Actions
- dotnet format

---

# Instalação

Clone o repositório:

```bash
git clone https://github.com/FernandoRoque91/DoseCertaCLI.git
```

Entre na pasta:

```bash
cd DoseCertaCLI
```

Restaure as dependências:

```bash
dotnet restore
```

---

# Execução

Execute o projeto:

```bash
dotnet run --project DoseCertaCLI
```

---

# Testes Automatizados

Execute os testes com:

```bash
dotnet test
```

---

# Linting / Análise Estática

Verificar formatação:

```bash
dotnet format --verify-no-changes
```

Corrigir automaticamente:

```bash
dotnet format
```

---

# Integração com API Pública

O projeto utiliza a API pública OpenFDA para realizar consultas online de medicamentos.

API utilizada:
https://open.fda.gov/apis/drug/

Tecnologias utilizadas na integração:

* HttpClient;
* requisições HTTP GET;
* desserialização JSON;
* testes de integração com mock HTTP.

---

# Estrutura do Projeto

```text
DoseCertaCLI/
│
├── DoseCertaCLI/
├── DoseCertaCLI.Tests/
├── .github/workflows/
├── README.md
├── .editorconfig
├── .gitignore
└── DoseCertaCLI.sln
```

---

# Como testar a integração da API

A funcionalidade de consulta online de medicamentos utiliza a API pública OpenFDA.

Para testar:

1. Execute o projeto:

```bash
dotnet run --project DoseCertaCLI
```

2. Escolha a opção:

```text
6 - Consultar medicamento online
```

3. Digite um medicamento válido, por exemplo:

```text
aspirin
```

ou

```text
tylenol
```

4. O sistema exibirá informações encontradas na base internacional OpenFDA.

Observação:
A funcionalidade requer conexão com a internet.

---

# Versionamento

Este projeto utiliza versionamento semântico:

```text
MAJOR.MINOR.PATCH
```

Versão atual:

```text
1.1.0
```

---

# Autor

Fernando Roque

---

# Repositório

https://github.com/FernandoRoque91/DoseCertaCLI