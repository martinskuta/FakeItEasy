namespace FakeItEasy.Core.Creation
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Responsible for creating dummy values.
    /// </summary>
    public interface IDummyValueCreator
    {
        /// <summary>
        /// Tries to create a dummy value of the specified type.
        /// </summary>
        /// <param name="type">The type of dummy to create.</param>
        /// <param name="dummy">An output parameter for the result.</param>
        /// <returns>True if a dummy could be created.</returns>
        [SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Justification = "Used by the framework, generics would provide no benefit.")]
        bool TryCreateDummyValue(Type type, out object dummy);
    }
}