using Gateway.GraphQL;
using Gateway.GraphQL.Schema.Queries;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClientServices(builder.Configuration);

builder.Services
    .AddScoped<Query>()
    .AddGraphQLServer()
    .AddQueryType<Query>();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGraphQL();

app.Run();
