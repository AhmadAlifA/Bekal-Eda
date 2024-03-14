using LookUp.Domain;
using Framework.Kafka;
using Lookup.GraphQL.Schema.Mutation;
using Lookup.GraphQL.Schema.Queries;
using LookUp.Domain.MapProfile;
using LookUp.Domain.Services;
using LookUp.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainContext(builder.Configuration);
builder.Services.AddKafkaProducer();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddScoped<Query>()
    .AddScoped<AttributeQuery>()
    .AddScoped<Mutation>()
    .AddScoped<AttributeMutation>()
    .AddScoped<IAttributeRepository, AttributeRepository>()
    .AddScoped<IAttributeService, AttributeService>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<AttributeQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<AttributeMutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
