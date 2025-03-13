using Microsoft.EntityFrameworkCore;
using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Repositories;
using OpenDataSigAPI.Services.OpenDataSig;
using Services.Functions.ContenedoresBasura;
using Services.Functions.Desfibriladores;
using Services.Functions.Farmacias;
using Services.Functions.PlazasMovilidadReducida;
using Services.Functions.PuntoLimpio;
using Services.OpenAi;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContextFactory
builder.Services.AddDbContextFactory<OpenDataSigAPIContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("AtencionUsuarios"));
    options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
}, lifetime: ServiceLifetime.Transient);

// Repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAgentsRepository, AgentsRepository>();
builder.Services.AddScoped<ILogsRepository, LogsRepository>();
builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
builder.Services.AddScoped<IRunsRepository, RunsRepository>();
builder.Services.AddScoped<IThreadsRepository, ThreadsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
builder.Services.AddScoped<IFilesRepository, FilesRepository>();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Otros Servicios
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<IOpenDataSigService, OpenDataSigService>();
builder.Services.AddHttpClient<IContenedoresBasuraService, ContenedoresBasuraService>();
builder.Services.AddHttpClient<IFarmaciasService, FarmaciasService>();
builder.Services.AddHttpClient<IPlazasMovilidadReducidaService, PlazasMovilidadReducidaService>();
builder.Services.AddHttpClient<IDesfibriladoresService, DesfibriladoresService>();
builder.Services.AddHttpClient<IPuntosLimpiosService, PuntosLimpiosService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
