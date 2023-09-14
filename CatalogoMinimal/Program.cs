using CatalogoMinimal.Endpoints;
using CatalogoMinimal.Extensions;
using CatalogoMinimal.Models;

#region Adicionar serviços ao container DI = ConfigureServices()
var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();
builder.AddPersistence();

builder.Services.AddCors();

builder.AddAuthentication();

var app = builder.Build();
#endregion

#region Endpoints

app.MapAutenticationEndpoint();

app.MapEntityEndpoints<Category, Guid>("/categoria");
app.MapEntityEndpoints<Product, Guid>("/produto");

#endregion

#region Configura a requisição do pipeline HTTP = Configure()

app.UseExceptionHandling(app.Environment)
    .UseAppCors()
    .UseSwaggerMiddleware();

//seguir nessa para aplicar a autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
#endregion

app.Run();