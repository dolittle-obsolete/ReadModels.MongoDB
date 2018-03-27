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