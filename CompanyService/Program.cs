using CompanyService.Models;
using Yatt.HttpClientService;
using YattCommon.Contracts;
using YattCommon.MongoDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMongo()
    .AddMongoRepository<UserModel>("Users")
    .AddMongoRepository<CatagoryModel>("Catagories")
    .AddMongoRepository<Company>("Companies");

builder.Services.AddHttpClient<IYattHttpClient<CompanyContract>, YattHttpClient<CompanyContract>>();

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
