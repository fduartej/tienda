## Comandos GIT Basicos

```
git init
git add \*
git commit -m "first commit"
git branch -M main
git remote add origin <URL DEL REPOSITORIO>
git push -u origin main

git config user.name "XXXXX"
git config user.email "xxxx@mail.com"
```

dotnet new mvc (PROYECTOS TRACIONALES MVC)

dotnet new mvc --auth Individual (PROYECTOS CON AUTH)

## Modificar los componentes de AUTH

dotnet aspnet-codegenerator identity -dc appcomics.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout;Account.ForgotPassword;Account.ResetPassword"

OPCIONAL

dotnet tool install --global dotnet-aspnet-codegenerator --version 8.0.7

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.7

## Comando de Migracion

dotnet ef migrations add PrimeraMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

dotnet ef database update

## SOLO en Caso no tengas instalado el EF

dotnet tool install --global dotnet-ef

## Nueva Migracion

dotnet ef migrations add CustomerMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

dotnet ef migrations add ProductoMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

dotnet ef migrations add OrdenPagoMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

dotnet ef migrations add PreOrdenMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

## Postggress

1. apptienda.csproj

agregar algunas librerias

2. appsettings.json

cambiado la cadena de conexion

3. Progrma.cs

Driver de Postgress

4. borrar las migraciones

dotnet ef migrations add PostgressMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

dotnet ef migrations add FixPostgressMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

## Code Generator

## generar crud producto

dotnet aspnet-codegenerator controller -name ProductoController -m Producto -dc apptienda.Data.ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet ef migrations add Fix2PosggressMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

dotnet ef migrations add MLContactMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"

dotnet ef migrations add RatingMigracion --context apptienda.Data.ApplicationDbContext -o "D:\Root\Code\usmp\20251\apptienda\Data\Migrations"
