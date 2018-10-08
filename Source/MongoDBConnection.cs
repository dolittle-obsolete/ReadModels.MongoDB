/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
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
            if (string.IsNullOrEmpty(config.ConnectionString))
            {
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
            }
            else
                Server = new MongoClient(config.ConnectionString);
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