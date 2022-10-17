using Microsoft.EntityFrameworkCore;
using TicTacToe.Core;
using TicTacToe.Data;
using WebApiTicTacToe.Contracts;
using WebApiTicTacToe.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GameState>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("MatricesContextConnectionStirngs")));
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBoardState, BoardState>();
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
