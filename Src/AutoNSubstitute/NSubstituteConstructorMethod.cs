using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using System.Linq;

namespace AutoNSubstitute
{
    public class NSubstituteConstructorMethod : IMethod
    {
        public NSubstituteConstructorMethod(Type type, IEnumerable<ParameterInfo> args)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            this.MockTargetType = type;
            Parameters = args;
        }

        public Type MockTargetType { get; private set; }

        public IEnumerable<ParameterInfo> Parameters { get; private set; }

        public object Invoke(IEnumerable<object> parameters)
        {
            return Substitute.For(new[] {MockTargetType}, parameters.ToArray());
        }
    }
}