using JobService.Models;
using Yatt.HttpClientService;
using YattCommon.Contracts;
using YattCommon.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMongo()
    .AddMongoRepository<Job>("Jobs")
    .AddMongoRepository<SubscribeModel>("Subscriptions")
    .AddMongoRepository<Qualification>("Qualifications")
    .AddMongoRepository<Duty>("Duties")
    .AddMongoRepository<Description>("Descriptions");

builder.Services.AddHttpClient<IYattHttpClient<JobContract>, YattHttpClient<JobContract>>();

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
