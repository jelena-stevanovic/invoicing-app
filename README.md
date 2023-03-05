## Ivoicing App

### Preconditions

1. SQL Server
2. Visual Studio
3. node/npm
4. NET Sdk 6.0

### Setting UP DB

1. Open Visual Studio sln file
2. Update connection string in appsettings file to match your local DB (currently it's set to SQLEXPRESS)
3. Using Package Manager Console switch default project to `InvoicingApp.Data`
4. Execute : `update-database` to apply migrations

### Running API project

1. Using Run or Debug option start `InvoicingApp.Api`
2. If Successfull you should see swagger page open on `localhost:5000`

### Running Client project

1. Navigate to `Client` directory
2. Execute `npm install`
3. Execute `npm start`
4. When finished in console you should see running client app link `localhost:4200`