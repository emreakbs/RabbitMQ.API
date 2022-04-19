using RabbitMQ.API;
using RabbitMQ.Consumer.Abstract;

var builder = WebApplication.CreateBuilder(args);

#region ConfigureService

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped();
#endregion
var app = builder.Build();

#region Configure
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
#endregion

