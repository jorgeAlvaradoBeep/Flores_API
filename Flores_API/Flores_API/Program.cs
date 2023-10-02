using Microsoft.AspNetCore.Hosting;
using System.Net.Sockets;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Flores_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

string localIP = LocalIPAddress();

//Conexion principal
//builder.Services.AddDbContext<TostatronicAPIContext>(
//options =>
//{
//    options.UseMySql(builder.Configuration.GetConnectionString("TostatronicBD"), // Poner "Pruebas"
//    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
//});

//Conexion de pruebas
builder.Services.AddDbContext<FloresAPIContext>(
options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("FloresBD"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});

var app = builder.Build();
app.Urls.Add("http://" + localIP + ":5249");//Uso Local


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

static string LocalIPAddress()
{
    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
    {
        socket.Connect("8.8.8.8", 65530);
        IPEndPoint? endPoint = socket.LocalEndPoint as IPEndPoint;
        if (endPoint != null)
        {
            return endPoint.Address.ToString();
        }
        else
        {
            return "127.0.0.1";
        }
    }
}