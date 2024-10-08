# Restauração do Banco de Dados e Configuração do Projeto


### Passo 1: Baixar o Arquivo do Banco de Dados

Faça o download do arquivo de backup do banco de dados, chamado  **"LivrariaBD.bak"**, disponível neste repositório.


### Passo 2: Restaurar o Banco de Dados


Abra o Microsoft SQL Server Management Studio (SSMS).
No explorador de objetos, clique com o botão direito em "Banco de Dados" e selecione a opção "Restaurar Banco de Dados...".
Selecione o arquivo "LivrariaBD.bak" que você baixou e siga as instruções na tela para concluir a restauração.

Exemplo visual:


![image](https://github.com/user-attachments/assets/7274557a-46a4-46f8-b517-868f46b8c10d)




![image](https://github.com/user-attachments/assets/4ddd894a-4ced-4def-813c-792ff4a14b8a)



### Passo 3: Alterar a String de Conexão


No diretório do projeto, abra o arquivo "appsettings.json".
Localize a chave "ConnectionStrings".
Substitua a string de conexão padrão pela string de conexão do seu ambiente de desenvolvimento.


Exemplo de configuração da string de conexão:


**"ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=LivrariaBD;Trusted_Connection=True;"
}**


![image](https://github.com/user-attachments/assets/0e4569c3-f0e4-40c7-80ad-e13698b104f4)



### Passo 4: Projeto Pronto para Executar


Após realizar a restauração do banco de dados e a configuração correta da string de conexão, o projeto estará pronto para rodar. Agora você pode compilar e executar o projeto no seu ambiente local.


![image](https://github.com/user-attachments/assets/f129e674-b7c4-46c4-92df-3c0b60f3a967)
![image](https://github.com/user-attachments/assets/7f5904ad-61c8-4b7f-9ef9-81be9d1f4e67)
![image](https://github.com/user-attachments/assets/1e2d8dcb-5e63-4469-9ca7-0ddbf901dd77)

