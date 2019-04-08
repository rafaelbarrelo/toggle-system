# Toggle System

Api application to configure toggles.
---

Steps to configure and run project:

1. Build the solution
```
dotnet build
```

2. Run the migrations
```
dotnet ef database update -c ToggleSystem.Infra.Data.Context.ToggleContext -p ./src/ToggleSystem.Infra.Data -s ./src/ToggleSystem.Api -v
```

3. Access your (localdb) and run the script **./docs/scripts.sql** to create some data

4. Import postman collection and enviroment from **./docs/postman-collections**

5. Open the solution and press F5 to run

6. Send the request "POST App1" from postman to authenticate user

7. Copy the JWT token generated

8. Open the "GET Get Toggles" request

9. In "Authorization" tab, select "Bearer Token" and paste the copied token.

10. Send the request

