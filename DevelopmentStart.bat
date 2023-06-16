start "Build CarService.UI" /D"%~dp0src\CarService.UI" dotnet build
start "Build CarService.UserAPI" /D"%~dp0src\CarService.UserAPI" dotnet build
start "Build CarService.MainAPI" /D"%~dp0src\CarService.MainAPI" dotnet build
TIMEOUT 5
start "Run CarService.UI" /D"%~dp0src\CarService.UI" dotnet run --environment Development --no-build
start "Run CarService.UserAPI" /D"%~dp0src\CarService.UserAPI" dotnet run --environment Development --no-build
start "Run CarService.MainAPI" /D"%~dp0src\CarService.MainAPI" dotnet run --environment Development --no-build
TIMEOUT 5
explorer "https://localhost:443"