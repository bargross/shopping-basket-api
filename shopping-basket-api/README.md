# How to run local development server

## Database dependencies
- install Docker Desktop https://www.docker.com/products/docker-desktop/
- install SQL2022 SSEI Dev (basic installation) https://go.microsoft.com/fwlink/p/?linkid=2215158&clcid=0x809&culture=en-gb&country=gb
- install SQL Server Management Studio (SSMS) https://aka.ms/ssms/21/release/vs_SSMS.exe

## Running the SQL Server container
- ensure Docker Desktop is running in the background as the SQL server will run in a Docker container
- navigate to the `shoipping-basket` directory in a terminal
- run the following command to start the SQL Server container:
```docker-compose up -d```
- this will start the SQL Server container in detached mode
- check docker desktop, see the container is running, likely called "portalserver"

## Connecting to the SQL Server container
- open SQL Server Management Studio (SSMS)
- connect to the server using the following details:
  - Server name: `localhost,1433`
  - Authentication: `SQL Server Authentication`
	- Login: 'sa'
	- Password: 'pass123!' (this is the password set in the `docker-compose.yml` file)
	- Connection Security Encryption: Optional