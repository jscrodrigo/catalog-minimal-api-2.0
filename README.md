# CatalogApi
> This is a basic Minimal API CRUD using .NET 6 with OpenAPI support (Swagger), JWT authenticantion and MySQL database connection. 
> It's great for begginers who wants to understand how Minimal APIs are built and  work. Enjoy it!

### Notes
+ To simplify the project, it's not using the popular `IIdentity` interface
+ All of the _Categoy_ endpoints' methods require authentication, so to use them, you need first call `/login` endpoint, using the following body:
  ```
  {
    "name": "user-test",
    "password": "password123"
  }
  ```
 - To change credentials, access _\Endpoints\AuthenticationEndpointsExtensions.cs_ and modify this `if` statment as needed:
    ```
    if (userModel.Name == "user-test" && userModel.Password == "password123")
    [...]
    ````
    A response token will be genetared. Use it in the Authorize section of the page (upper right corner)
