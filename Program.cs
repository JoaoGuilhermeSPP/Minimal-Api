
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", static ([FromBody] miminal_api.DTOs.LoginDTO loginDTO) =>
{
    if (loginDTO.Email == "adm.teste@gmail.com" && loginDTO.Senha == "123456")
        return Results.Ok("Login com sucesso");
    else
        return Results.Unauthorized();
});
app.Run();
public class LoginDTO() {
    public string Email { get; set; } = default;
    public string Senha { get; set; } = default;
}


