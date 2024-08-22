using Finance.API.Application.Requests;
using Finance.API.Application.Services;
using Finance.API.Domain.Entities;
using Finance.API.Domain.Repositories;
using Finance.API.Filters;
using Finance.API.Infrastructure.Persistence;
using Finance.API.Infrastructure.Repositories;
using Finance.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<AppDbContext>()
.AddUserManager<UserManager<User>>()// Adicione o SignInManager manualmente
.AddDefaultTokenProviders();

builder.Services.AddValidatorsFromAssemblyContaining<AddTransactionValidator>();
builder.Services.AddTransient<IValidator<AddTransactionRequest>, AddTransactionValidator>();


builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


builder.Services.AddControllers();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("#@#@!323@$#@$^%$&^&5DSfds!!$#$AC!!XZ!!C#$#@FDSADE#")),
        ClockSkew = TimeSpan.Zero
    };
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Barber Manager API", Version = "v1" });

    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    // Configuração para autenticação JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira o token JWT com o prefixo Bearer. Exemplo: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
}

);

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AppCors", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Permite a origem específica do frontend
              .AllowAnyHeader()                     // Permite qualquer cabeçalho
              .AllowAnyMethod()        // Permite qualquer método (GET, POST, etc.)                
              .AllowCredentials(); //Permite o envio de credenciais (cookies, tokens, etc.)
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AppCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
