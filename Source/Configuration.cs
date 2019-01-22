/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using Dolittle.ResourceTypes.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// Represents the configuration for MongoDB
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Configuration"/> 
        /// </summary>
        /// <param name="configurationWrapper"></param>
        public Configuration(IConfigurationFor<ReadModelRepositoryConfiguration> configurationWrapper)
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
                Client = new MongoClient(s);
            }
            else
                Client = new MongoClient(config.ConnectionString);
            Database = Client.GetDatabase(config.Database);

            BsonSerializer.RegisterSerializationProvider(new ConceptSerializationProvider());
        }

        /// <summary>
        /// Get the <see cref="IMongoClient"/>
        /// </summary>
        public IMongoClient Client { get; }

        /// <summary>
        /// Get the current <see cref="IMongoDatabase"/>
        /// </summary>
        public IMongoDatabase Database { get; }
    }
}