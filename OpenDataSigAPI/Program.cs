using Microsoft.EntityFrameworkCore;
using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Repositories;
using OpenDataSigAPI.Services.OpenDataSig;
using Services.OpenAi;
using Services.Functions.Farmacias;
using Services.Functions.ContenedoresBasura;
using Services.Functions.PlazasMovilidadReducida;
using Services.Functions.Desfibriladores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContextFactory (IMPORTANTE)
builder.Services.AddDbContextFactory<OpenDataSigAPIContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("AtencionUsuarios"));
    options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
}, lifetime: ServiceLifetime.Transient);

// Register repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAgentsRepository, AgentsRepository>();
builder.Services.AddScoped<ILogsRepository, LogsRepository>();
builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
builder.Services.AddScoped<IRunsRepository, RunsRepository>();
builder.Services.AddScoped<IThreadsRepository, ThreadsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
builder.Services.AddScoped<IFilesRepository, FilesRepository>();

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register other services
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddScoped<IOpenDataSigService, OpenDataSigService>();
builder.Services.AddHttpClient<IContenedoresBasuraService, ContenedoresBasuraService>();
builder.Services.AddHttpClient<IFarmaciasService, FarmaciasService>();
builder.Services.AddHttpClient<IPlazasMovilidadReducidaService, PlazasMovilidadReducidaService>();
builder.Services.AddHttpClient<IDesfibriladoresService, DesfibriladoresService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
