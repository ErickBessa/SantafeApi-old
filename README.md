# SantafeApi
_*Passo a passo para rodar o projeto:*_ 
1. git clone https://github.com/breno-sapucaia/SantafeApi.git
2. Abrir a solução com Visual Studio
3. Efetuar o *Restore* da aplicação
4. Fazer o backup do DbSantaHelena.bak no Localdb ( importante )
5. Efetuar o comando `Update-Database` no package manager console par atualizar o banco de dados
6. Rodar o programa e testar as rotas


# Database

Be sure to have your .bak file in hands!

Run this command to start your sql-server

```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=rootAdmin123' \
   --name 'santafe-sql-server' -p 1401:1433 \
   -v santafe-sql:/var/opt/mssql \
   -d mcr.microsoft.com/mssql/server:2019-latest
```
After having your sql server running 
create a folder that will had the backup
```
sudo docker exec -it santafe-sql-server mkdir /var/opt/mssql/backup
```
Then run this command to copy the .bak inside the container:

```
sudo docker cp DbSantaHelena.bak santafe-sql-server:/var/opt/mssql/backup
```

Once you had it copied you must run this command:

```bash
sudo docker exec -it santafe-sql-server /opt/mssql-tools/bin/sqlcmd -S localhost \
   -U SA -P 'rootAdmin123' \
   -Q 'RESTORE FILELISTONLY FROM DISK = "/var/opt/mssql/backup/DbSantaHelena.bak"' \
   | tr -s ' ' | cut -d ' ' -f 1-2
```
Then
```bash
sudo docker exec -it santafe-sql-server -S localhost -U SA -P 'rootAdmin123' -Q 'RESTORE DATABASE DbSantaHelena FROM DISK="/opt/mssql/backup/DbSantaHelena.bak" WITH MOVE "DbSantaHelena" TO "/opt/mssql/backup/DbSantaHelena.mdf", MOVE "DbSantaHelena_log" TO "/opt/mssql/backup/DbSantaHelena_log.ldf";'
```

