namespace FakeItEasy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using FakeItEasy.Configuration;

    internal class ArgumentConstraintTrap
        : IArgumentConstraintTrapper
    {
        [ThreadStatic]
        private static List<IArgumentConstraint> trappedConstraints;

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CallTo", Justification = "It's an identifier")]
        public static void ReportTrappedConstraint(IArgumentConstraint constraint)
        {
            if (trappedConstraints is null)
            {
                throw new InvalidOperationException("A<T>.Ignored, A<T>._, and A<T>.That can only be used in the context of a call specification with A.CallTo()");
            }

            trappedConstraints.Add(constraint);
        }

        public IArgumentConstraint TrapConstraintOrCreate(
            Action actionThatProducesConstraint,
            Func<IArgumentConstraint> defaultConstraintFactory)
        {
            trappedConstraints = new List<IArgumentConstraint>();
            var result = trappedConstraints;

            actionThatProducesConstraint.Invoke();

            trappedConstraints = null;

            return result.Count == 0 ? defaultConstraintFactory.Invoke() :
                result.Count == 1 ? result[0] :
                throw new FakeConfigurationException(ExceptionMessages.TooManyArgumentConstraints(result[1]));
        }
    }
}
