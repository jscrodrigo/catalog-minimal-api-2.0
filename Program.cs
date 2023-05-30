using Catalog.AppServicesExtensions;
using Catalog.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddApiSwagger();
builder.AddPersistance();
builder.Services.AddCors();
builder.AddJwtAuthentication();

var app = builder.Build();

app.MapAuthenticationEndpoints();
app.MapCategoryEndpoints();
app.MapProductEndpoints();

var environment = app.Environment;

app.UseExceptionHandling(environment)
   .UseSwaggerMiddleware()
   .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

