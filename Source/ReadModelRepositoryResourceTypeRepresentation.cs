using System;
using System.Collections.Generic;
using Dolittle.Resources;

namespace Dolittle.ReadModels.MongoDB
{
    /// <inheritdoc/>
    public class ReadModelRepositoryResourceTypeRepresentation : IRepresentAResourceType
    {
        IDictionary<Type, Type> _bindings;
        
        /// <inheritdoc/>
        public ResourceType Type => "ReadModels";
        /// <inheritdoc/>
        public ResourceTypeImplementation ImplementationName => "MongoDB";
        /// <inheritdoc/>
        public Type ConfigurationObjectType => typeof(ReadModelRepositoryConfiguration);
        /// <inheritdoc/>
        public IDictionary<Type, Type> Bindings {
            get 
            {
                if (_bindings == null) 
                    InitializeBindings();
                
                return _bindings;
           }
        }

        void InitializeBindings()
        {
            _bindings = new Dictionary<Type, Type>();
            _bindings.Add(typeof(IReadModelRepositoryFor<>), typeof(ReadModelRepositoryFor<>));
        }
    }
}