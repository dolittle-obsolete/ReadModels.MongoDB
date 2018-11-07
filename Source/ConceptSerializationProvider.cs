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
            {
                var createConceptSerializerGenericMethod = this.GetType().GetMethod("CreateConceptSerializer").MakeGenericMethod(type);
                dynamic serializer = createConceptSerializerGenericMethod.Invoke(null, new object[]{});
                return serializer;
            }
                
            return null;
        }
        /// <summary>
        /// Creates an instance of a serializer of the concept of the given type param T
        /// </summary>
        /// <typeparam name="T">The Concept type</typeparam>
        /// <returns></returns>
        public static ConceptSerializer<T> CreateConceptSerializer<T>()
        {
            return new ConceptSerializer<T>();
        }
    }
}