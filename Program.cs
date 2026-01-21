using System.Text;
using Message_Api.Data;
using Message_Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MessageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerServices();
builder.Services.AddCorsServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Message Api");
    c.RoutePrefix = string.Empty;
});
app.MapControllers();
app.UseCors("AllowBlazor");
app.UseAuthentication();
app.UseAuthorization();
app.Run();