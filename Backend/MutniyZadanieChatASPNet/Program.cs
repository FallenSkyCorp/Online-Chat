using Chat.Core.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MutniyZadanieChatASPNet.API.Hubs;
using Users.Application.Services;
using Users.DataAccess;
using Users.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");

    options.Configuration = connection;
});

builder.Services.AddControllers();

builder.Services.AddDbContext<UsersDbContext>( // связываем с проектом базы данных
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(UsersDbContext)) // указываем строку подключения к базе данных(лежит в appsetings.json)
        ); // указываем что база данных это Postgres
    });

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCors",
         builder =>
         {
             builder.SetIsOriginAllowed((host) => true)
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials();
         });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseWebSockets();

app.UseCors("MyCors");

app.MapHub<ChatHub>("/chat");

app.MapControllers();


app.Run();
