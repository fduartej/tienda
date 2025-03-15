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
