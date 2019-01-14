/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.ResourceTypes;
using MongoDB.Driver;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// Represents a definition of a resource type for MongoDB ReadModels
    /// </summary>
    public class ResourceTypeRepresentation : IRepresentAResourceType
    {
        static IDictionary<Type, Type> _bindings = new Dictionary<Type, Type>
        {
            {typeof(IReadModelRepositoryFor<>), typeof(ReadModelRepositoryFor<>)},
            {typeof(IAsyncReadModelRepositoryFor<>), typeof(AsyncReadModelRepositoryFor<>)}
        };
        
        /// <inheritdoc/>
        public ResourceType Type => "readModels";

        /// <inheritdoc/>
        public ResourceTypeImplementation ImplementationName => "MongoDB";

        /// <inheritdoc/>
        public Type ConfigurationObjectType => typeof(ReadModelRepositoryConfiguration);
        
        /// <inheritdoc/>
        public IDictionary<Type, Type> Bindings => _bindings;
    }
}