using AuthenticationManager;
using IdentityService.Handlers;
using IdentityService.Models;
using Yatt.HttpClientService;
using YattCommon.Contracts;
using YattCommon.MongoDb;
using YattCommon.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtSettingsSection = builder.Configuration.GetSection("JWTSettings");
var jwtSettings = jwtSettingsSection.Get<JWTSettings>();

ServiceSetting? serviceSetting=builder.Configuration
    .GetSection(nameof(ServiceSetting)).Get<ServiceSetting>();

builder.Services.AddMongo()
    .AddMongoRepository<User>("Users")
    .AddMongoRepository<UserToken>("UserTokens")
    .AddMongoRepository<RefreshToken>("RefreshTokens");

builder.Services.AddHttpClient<IYattHttpClient<IdentityContract>, YattHttpClient<IdentityContract>>(); 

//builder.Services.AddScoped<IYattHttpClient<IdentityContract>, YattHttpClient<IdentityContract>>();

//builder.Services.AddScoped<IMessageProducer, MessageProducer>();
//builder.Services.AddRabbitMqExtension()
//    .AddMessageProducer<IdentityContract>("identity");

builder.Services.Configure<JWTSettings>(jwtSettingsSection);

builder.Services.AddCustomJwtAuthentication(jwtSettings.SecretKey);

builder.Services.AddSingleton<TokenManager>();

builder.Services.AddControllers(opt =>
{
    opt.SuppressAsyncSuffixInActionNames = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
