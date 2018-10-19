/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;
using MongoDB.Bson.Serialization;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class ConceptSerializationProvider : IBsonSerializationProvider
    {
        /// <inheritdoc/>
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type.IsConcept())
                return new ConceptSerializer(type);

            return null;
        }
    }
}