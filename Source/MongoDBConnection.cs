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
        /// <param name="config"></param>
        public Connection(Configuration config)
        {
            var s = MongoClientSettings.FromUrl(new MongoUrl(config.Url));
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
            Database = Server.GetDatabase(config.DefaultDatabase);

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