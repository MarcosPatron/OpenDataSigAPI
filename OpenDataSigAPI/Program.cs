using OpenDataSigAPI.Services.OpenDataSig;
using Services.OpenAi;
using Services.Functions.Farmacias;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ REGISTRAR SERVICIOS CORRECTAMENTE

builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<IOpenDataSigService, OpenDataSigService>();
builder.Services.AddHttpClient<IFarmaciasService, FarmaciasService>();

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
