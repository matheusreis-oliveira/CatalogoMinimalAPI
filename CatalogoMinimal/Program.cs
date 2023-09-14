using CatalogoMinimal.Endpoints;
using CatalogoMinimal.Extensions;
using CatalogoMinimal.Models;

#region Adicionar servi�os ao container DI = ConfigureServices()
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

#region Configura a requisi��o do pipeline HTTP = Configure()

app.UseExceptionHandling(app.Environment)
    .UseAppCors()
    .UseSwaggerMiddleware();

//seguir nessa para aplicar a autentica��o e autoriza��o
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
#endregion

app.Run();