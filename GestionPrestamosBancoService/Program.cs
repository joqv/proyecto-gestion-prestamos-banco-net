using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Data.Respositorio;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Dependencias
builder.Services.AddScoped<ICliente, ClienteRepositorio>();
builder.Services.AddScoped<IPrestamo, PrestamoRepositorio>();
builder.Services.AddScoped<ICuotaPrestamo, CuotaPrestamoRepositorio>();
builder.Services.AddScoped<ICuentaBancaria, CuentaBancariaRepositorio>();
builder.Services.AddScoped<IMoneda, MonedaRepositorio>();
builder.Services.AddScoped<ITipoCuenta, TipoCuentaRepositorio>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();

// Clave token llamado
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

// Configurar autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
       /* options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                var result = System.Text.Json.JsonSerializer.Serialize(new
                {
                    message = "Necesita logearse o faltan permisos"
                });

                await context.Response.WriteAsync("[]");
            }
        };*/
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseAuthorization();

app.MapControllers();

app.Run();
