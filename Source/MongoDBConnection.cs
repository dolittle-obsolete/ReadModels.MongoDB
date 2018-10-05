using Dolittle.Resources.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationWrapper"></param>
        public Connection(IConfigurationFor<ReadModelRepositoryConfiguration> configurationWrapper)
        {
            var config = configurationWrapper.Instance;
            var s = MongoClientSettings.FromUrl(new MongoUrl(config.Host));
            if (config.UseSSL)
            {
                s.UseSsl = true;
                s.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                    CheckCertificateRevocation = false
                };
            }

            Server = new MongoClient(s);
            Database = Server.GetDatabase(config.Database);

            BsonSerializer.RegisterSerializationProvider(new ConceptSerializationProvider());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MongoClient Server { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IMongoDatabase Database { get; }
    }
}