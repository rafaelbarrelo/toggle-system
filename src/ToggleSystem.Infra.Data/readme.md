# EF Migrations command helper

Run this commands in project root directory

### List available migrations

```
dotnet ef migrations list -c ToggleSystem.Infra.Data.Context.ToggleContext -p ./src/ToggleSystem.Infra.Data -s ./src/ToggleSystem.Api
```

### Add new migration

```
dotnet ef migrations add Initial -c ToggleSystem.Infra.Data.Context.ToggleContext -p ./src/ToggleSystem.Infra.Data -s ./src/ToggleSystem.Api -v
```

### Update database

```
dotnet ef database update -c ToggleSystem.Infra.Data.Context.ToggleContext -p ./src/ToggleSystem.Infra.Data -s ./src/ToggleSystem.Api -v
``` 
