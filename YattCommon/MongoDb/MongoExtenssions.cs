using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using YattCommon.Settings;

namespace YattCommon.MongoDb
{
    public static class MongoExtenssions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                ServiceSetting? serviceSetting = configuration!.GetSection(nameof(ServiceSetting))
                    .Get<ServiceSetting>();
                MongoDbSetting? mongoDbSetting = configuration!.GetSection(nameof(MongoDbSetting))
                    .Get<MongoDbSetting>();

                var mongoClient = new MongoClient(mongoDbSetting!.ConnectionName);
                return mongoClient.GetDatabase(serviceSetting!.ServiceName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services,string collectionName)
            where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {
                var database=serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database!, collectionName);
            });

            return services;
        }
    }
}
