using System;
using Dolittle.Resources;

namespace Dolittle.ReadModels.MongoDB
{
    /// <inheritdoc/>
    public class ReadModelRepositoryResourceDefinition : ICanDefineAResource
    {
        
        /// <inheritdoc/>
        public ResourceType ResourceType => new ResourceType{Value = "readModels"};
        /// <inheritdoc/>
        public ResourceTypeName ResourceTypeName => new ResourceTypeName{Value = "MongoDB"};
        /// <inheritdoc/>
        public Type ConfigurationObjectType => typeof(ReadModelRepositoryConfiguration);
    }
}