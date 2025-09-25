using Autofac.Extensions.DependencyInjection;
using Data.Repository.Talonario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Talonario.Api.Server.Application;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Services;
using Talonario.Api.Server.Infrastructure.Repositories;
using Talonario.Api.Server.InfraStructure.Repository;

var builder = WebApplication.CreateBuilder(args);

IWebHostEnvironment environment = builder.Environment;
ConfigurationManager configuration = builder.Configuration;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Logging.AddJsonConsole();
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

//configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//configuration.AddJsonFile($"appsettings.Dev.json", optional: true);

builder.Services.AddScoped<SqlConnection>(_ => new SqlConnection(configuration.GetConnectionString("AutenticadorDataBase")));
builder.Services.AddScoped<SqlConnection>(_ => new SqlConnection(configuration.GetConnectionString("AtelierDataBase")));

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("talonario-api", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Talonário Eletrônico (AIT)",
        Description = ""
    });

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(s: configuration["Jwt:SecurityKey"] ?? string.Empty)
            )
        };
    });

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

//Services
builder.Services.AddTransient<ICondutorApplicationService, CondutorApplicationService>();
builder.Services.AddTransient<IGoogleMapsApplicationService, GoogleMapsApplicationService>();
builder.Services.AddTransient<IInfracaoApplicationService, InfracaoApplicationService>();
builder.Services.AddTransient<IObservacaoService, ObservacaoService>();
builder.Services.AddTransient<IPessoaApplicationService, PessoaApplicationService>();
builder.Services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();
builder.Services.AddTransient<IVeiculoApplicationService, VeiculoApplicationService>();
builder.Services.AddTransient<ICampanhasTalonarioRepository, CampanhasTalonarioRepository>();
builder.Services.AddScoped<ITcTamaParametrosService, TcTamaParametrosService>();
builder.Services.AddScoped<ITermoService, TermoService>();
builder.Services.AddScoped<TermoService>();
builder.Services.AddScoped<ITamaService, TamaService>();
builder.Services.AddScoped<ITermoAdocaoService, TermoAdocaoService>();
builder.Services.AddScoped<ICancelamentoAITService, CancelamentoAITService>();
builder.Services.AddScoped<ILogsService, LogsService>();
//Repositories
builder.Services.AddTransient<ICondutorRepository, CondutorRepository>();
builder.Services.AddTransient<IInfracaoRepository, InfracaoRepository>();
builder.Services.AddTransient<IObservacaoRepository, ObservacaoRepository>();
builder.Services.AddTransient<IOrgaoAutuadorRepository, OrgaoAutuadorRepository>();
builder.Services.AddTransient<IPessoaRepository, PessoaRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddTransient<ICampanhasTalonarioService, CampanhaApplicationService>();
builder.Services.AddScoped<ITcTamaParametrosRepository, TcTamaParametrosRepository>();
builder.Services.AddScoped<ITermoRepository, TermoRepository>();
builder.Services.AddScoped<ITamaRepository, TamaRepository>();
builder.Services.AddScoped<ITermoAdocaoRepository, TermoAdocaoRepository>();
builder.Services.AddScoped<ICancelamentoAITRepository, CancelamentoAITRepository>();
builder.Services.AddScoped<ILogsRepository, LogsRepository>();

//// Jobs -- em teste
//builder.Services.AddHostedService<EmplacamentoJob>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentname}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(url: "/swagger/talonario-api/swagger.json", name: "Talonário API Service");
    c.RoutePrefix = "swagger";
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapSwagger();

app.Run();