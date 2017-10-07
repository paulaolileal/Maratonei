# PUC Minas 2017
- Autores: Paulo Victor de Oliveira Leal, Augusto de Castro Vieira, Vinicius Luiz Lopes Nunes
- Ciência da Computação - Otimização de Sistemas

## Objetivo
- Desenvolver um servidor capaz de calcular o Simplex para uma função com n variaveis de decisão e n restrições. O aplicativo comunica com o servidor (hospedado no Azure) através de requisições http, usando Json.

### Observação
- Visual Studio 2017 Enterprise
- .NET Core 2.0
- Testado somente no Windows

### Execução no Windows
- Instale o .NET Core de acordo com o seu PC (x64 ou x86) no link:
    - [.Net Core - Windows]
- Abra o prompt de comando 
- Navegue até a pasta do projeto
	- Onde esta o arquivo "Maratonei.csproj"
- Execute o comando: 
    ```
    $ dotnet restore
    $ dotnet run
    ```

### Execução no linux
- Instale o .NET Core de acordo com a sua distribuição:
    - [.Net Core - Linux]
- Navegue até a pasta do projeto
	- Onde esta o arquivo "Maratonei.csproj"
- Execute o comando: 
    ```
    $ dotnet restore
    $ dotnet run
    ```

### Teste
- Para testar:
- Abra o navegador na url informada durante a compilação: 
    - http://localhost:.../
    - Se a pagina inicial for exibida o servidor está funcionando
- Abra o Postman e ultilize as requisições da coleção:
    - [PostmanCollection]  

### Distribuição dos arquivos

- Models:
    -  Estrutura da função objetiva (ObjectiveFunction.cs)
    -  Estrutura das restrições (Restrictio.cs)
-  Controller:
    -  Controle das requisições (SimplexController.cs)
    -  Components:
        -  Classe que efetua o calculo do Simplex (Simplex.cs)

[PostmanCollection]: <https://www.getpostman.com/collections/48c7b85bb5c71c73a113>
[.Net Core - Windows]: <https://www.microsoft.com/net/download/core>
[.Net Core - Linux]: <https://www.microsoft.com/net/download/linux>