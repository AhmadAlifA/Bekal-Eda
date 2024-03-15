using Payment.Domain.Repositories;
using Payment.Domain.Services;
using Payment.Domain;
using Payment.GraphQL.Schema.Mutation;
using Payment.GraphQL.Schema.Queries;
using Framework.Core.Events;
using Framework.Kafka;
using Payment.Domain.MapProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainContext(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", opt =>
    {
        var Configuration = builder.Configuration;

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = Configuration["JWT:ValidIssuer"],
            ValidAudience = Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
        };
        opt.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.Response.OnStarting(async () =>
                {
                    await context.Response.WriteAsync("You are not authorized!");
                });
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                context.Response.OnStarting(async () =>
                {
                    await context.Response.WriteAsync("You are forbidden!");
                });
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin",
    builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    }));

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<EntityToDtoProfile>();
});

builder.Services.AddPayment();
builder.Services.AddEventBus();
builder.Services.AddKafkaProducer();
builder.Services.AddKafkaConsumer();

builder.Services
    .AddScoped<Query>()
    .AddScoped<Mutation>()
    .AddScoped<PaymentQuery>()
    .AddScoped<PaymentMutation>()
    .AddScoped<IPaymentService, PaymentService>()
    .AddScoped<IPaymentRepository, PaymentRepository>()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<PaymentQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<PaymentMutation>()
    .AddAuthorization();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAnyOrigin");

app.MapGraphQL();

app.Run();