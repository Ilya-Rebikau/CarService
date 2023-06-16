# Car service
## Car service application that provides different abilities for users.
### How to use:
1. To use this application you need to download this solution;
2. Create database from project CarService.Database;
3. Change path to your SQL server in projects:
    - CarService.UI in file [appsettings.json](https://github.com/EPAM-Gomel-NET-Lab/IlyaRebikau/blob/develop/src/TicketManagement.Web/appsettings.json "Database config") (Server=.\\SQLEXPRESS change to Server=*path to your SQL server*);
    - CarService.MainAPI in file [appsettings.json](https://github.com/EPAM-Gomel-NET-Lab/IlyaRebikau/blob/develop/src/TicketManagement.UserAPI/appsettings.json "Database config") (Server=.\\SQLEXPRESS change to Server=*path to your SQL server*);
    - CarService.UserAPI in file [appsettings.json](https://github.com/EPAM-Gomel-NET-Lab/IlyaRebikau/blob/develop/src/TicketManagement.EventManagerAPI/appsettings.json "Database config") (Server=.\\SQLEXPRESS change to Server=*path to your SQL server*);
4. To run all needed projects and start working with them were created 2 batch files:
    - [DevelopmentStart.bat](https://github.com/EPAM-Gomel-NET-Lab/IlyaRebikau/blob/develop/DebugStart.bat "Run app in development mode") to run application in development mode.
    - [ProductionStart.bat](https://github.com/EPAM-Gomel-NET-Lab/IlyaRebikau/blob/develop/ReleaseStart.bat "Run app in release mode") to run application in production mode.

### Credentials:
There are 4 roles in this application, you can use them.
1. Admin account - login: admin@mail.ru, password: Qwer!1
2. Manager account - login: manager@mail.ru, password: Qwer!1
3. User account - login: user@mail.ru, password: Qwer!1
4. Guest account  
