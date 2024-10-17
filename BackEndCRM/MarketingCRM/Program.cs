using Application.AutoMapper;
using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.UseCase;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Marketing CRM", Version = "1.0" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//Custom

builder.Services.AddDbContext<CRMDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql")));

//CQRS

builder.Services.AddScoped<IClientsCommand, ClientsCommand>();
builder.Services.AddScoped<IClientsQuery, ClientsQuery>();
builder.Services.AddScoped<ICampaignTypesQuery, CampaignTypesQuery>();
builder.Services.AddScoped<IInteractionTypesQuery, InteractionTypesQuery>();
builder.Services.AddScoped<ITaskStatusQuery, TaskStatusQuery>();
builder.Services.AddScoped<IUsersQuery, UsersQuery>();
builder.Services.AddScoped<IProjectsCommand, ProjectsCommand>();
builder.Services.AddScoped<IProjectsQuery, ProjectsQuery>();
builder.Services.AddScoped<IInteractionsCommand, InteractionsCommand>();
builder.Services.AddScoped<IInteractionsQuery, InteractionsQuery>();
builder.Services.AddScoped<ITasksCommand, TasksCommand>();
builder.Services.AddScoped<ITasksQuery, TasksQuery>();

//Services

builder.Services.AddScoped<IServiceClients, ServiceClients>();
builder.Services.AddScoped<IServiceCampaignTypes, ServiceCampaignTypes>();
builder.Services.AddScoped<IServiceInteractionTypes, ServiceInteractionTypes>();
builder.Services.AddScoped<IServiceTaskStatus, ServiceTaskStatus>();
builder.Services.AddScoped<IServiceUsers, ServiceUsers>();
builder.Services.AddScoped<IServiceProjects, ServiceProjects>();
builder.Services.AddScoped<IServiceInteractions, ServiceInteractions>();
builder.Services.AddScoped<IServiceTasks, ServiceTasks>();

//AUTOMAPPER

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));


//CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marketing CRM v1.0");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
