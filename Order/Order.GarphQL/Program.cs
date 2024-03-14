using Order.Domain;
using Order.Domain.MapProfile;
using Framework.Core.Events;
using Framework.Kafka;
using Order.GarphQL.Schema.Queries;
using Order.GarphQL.Schema.Mutation;
using Order.Domain.Repositories;
using Order.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainContext(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});

builder.Services.AddOrder();
builder.Services.AddEventBus();
builder.Services.AddKafkaProducer();
builder.Services.AddKafkaConsumer();

builder.Services
    .AddScoped<Query>()
    .AddScoped<Mutation>()
    .AddScoped<CartQuery>()
    .AddScoped<ProductQuery>()
    .AddScoped<CartMutation>()
    .AddScoped<CartProductMutation>()
    .AddScoped<ICartRepository, CartRepository>()
    .AddScoped<ICartProductRepository, CartProductRepository>()
    .AddScoped<ICartService, CartService>()
    .AddScoped<ICartProductService, CartProductService>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<CartQuery>()
    .AddTypeExtension<CartProductQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<CartProductMutation>()
    .AddTypeExtension<CartMutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
