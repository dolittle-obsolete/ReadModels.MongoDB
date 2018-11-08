using System;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// The exception that gets thrown when the <see cref="ConceptSerializer{T}"/> failed serializing a concept
    /// </summary>
    public class FailedConceptSerialization : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="FailedConceptSerialization"/>
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public FailedConceptSerialization(string msg) : base(msg)
        {

        }
    }
}