using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Data.Respositorio;

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
