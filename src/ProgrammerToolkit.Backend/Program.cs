using ChatGptBackEnd.GptProvider;
using ChatGptBackEnd.GptRepository;
using ProgrammerToolkit.Backend.IProvider;
using ProgrammerToolkit.Core.Errors;
using ProgrammerToolkitBackend.IProvider;
using ProgrammerToolkitBackend.Provider;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add DI

#region Singleton
builder.Services.AddSingleton<IErrorMap, ErrorMapBase>();
#endregion
#region Scoped
builder.Services.AddScoped<IWebToolsProvider, WebToolsProvider>();
builder.Services.AddScoped<IGptMessageProvider, GptMessageProvider>();
builder.Services.AddScoped<ICallGptRepository,CallGptRepository>();
builder.Services.AddScoped<IRandomToolsProvider, RandomToolsProvider>();
#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
