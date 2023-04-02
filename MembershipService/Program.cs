using MembershipService.Models;
using MembershipService.Settings;
using Yatt.HttpClientService;
using YattCommon.Contracts;
using YattCommon.MongoDb;
using YattCommon.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ServiceSetting? serviceSetting = builder.Configuration
    .GetSection(nameof(ServiceSetting)).Get<ServiceSetting>();

builder.Services.AddMongo()
    .AddMongoRepository<Membership>("Memberships");

builder.Services.AddHttpClient<IYattHttpClient<MembershipContract>, YattHttpClient<MembershipContract>>();


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
