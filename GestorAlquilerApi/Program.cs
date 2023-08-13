using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using GestorAlquilerApi.DataAccessLayer.Data;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.ControllersService;
using GestorAlquilerApi.DataAccessLayer.Repository;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.DTOs;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{

    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
            .WithOrigins("*") // Agrega el dominio del frontend
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        }
    );


});

//////////// Add services to the container.//////////////////

//Dependency injection for Bussiness Logic Layer
builder.Services.AddScoped(typeof(IGenericService<BranchDTO>), typeof(BranchesServices<BranchDTO>));
builder.Services.AddScoped<IQueryBranch, BranchQueries>();

builder.Services.AddScoped(typeof(IGenericService<CarDTO>), typeof(CarService<CarDTO>));
builder.Services.AddScoped<IQueryCar, CarQueries>();

builder.Services.AddScoped(typeof(IGenericService<ClientDTO>), typeof(ClientService<ClientDTO>));
builder.Services.AddScoped<IQueryClient, ClientQueries>();

builder.Services.AddScoped(
    typeof(IGenericService<PlanningDTO>),
    typeof(PlanningService<PlanningDTO>)
);
builder.Services.AddScoped<IQueryPlanning, PlanningQueries>();

builder.Services.AddScoped(
    typeof(IGenericService<ReservationDTO>),
    typeof(ReservationService<ReservationDTO>)
);
builder.Services.AddScoped<IQueryReservation, ReservationQueries>();

//Dependency injection for Data Acces Layer
builder.Services.AddScoped(typeof(ISaveData<Branch>), typeof(SaveData<Branch>));
builder.Services.AddScoped(typeof(ISaveData<Car>), typeof(SaveData<Car>));
builder.Services.AddScoped(typeof(ISaveData<Client>), typeof(SaveData<Client>));
builder.Services.AddScoped(typeof(ISaveData<Planning>), typeof(SaveData<Planning>));
builder.Services.AddScoped(typeof(ISaveData<Reservation>), typeof(SaveData<Reservation>));

//////////
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ISetAdminService, SetAdminService>();
builder.Services.AddScoped<ICustomService, CustomService>();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//DBContext
builder.Services.AddDbContext<ApiContext>(
    options =>
        options.UseSqlite(
            builder.Configuration.GetConnectionString("ApiContext")
                ?? throw new InvalidOperationException("Connection string 'ApiContext' not found.")
        )
);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Version = "v1",
            Title = "Api Gestion De Alquileres",
            Description =
                "An ASP.NET Core Web API to carry out the management of branches, cars, clients and reservations in a car rental company",
            Contact = new OpenApiContact
            {
                Name = "Muhammad Hicho Haidor",
                Email = "mh.haidor@gmail.com",
                Url = new Uri("https://github.com/Mohasb")
            }
        }
    );
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    //Add Security JWT to swagger
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
    );
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                new string[] { }
            }
        }
    );
});

//JWT Especifications
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")
            )
        };
    });

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Use((context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = StatusCodes.Status200OK;
        return context.Response.WriteAsync("OK");
    }

    return next();
});


//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
