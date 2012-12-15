using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture.Kernel;

namespace AutoNSubstitute
{
    public class NSubstituteConstructorQuery : IMethodQuery, IConstructorQuery
    {
        public IEnumerable<IMethod> SelectConstructors(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return Foo(type);
        }

        public IEnumerable<IMethod> SelectMethods(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return Foo(type);
        }

        private static IEnumerable<IMethod> Foo(Type type)
        {
            if (!type.IsInterface)
            {
                var constructorInfos =
                    type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                return constructorInfos.Select(c => new NSubstituteConstructorMethod(type, c.GetParameters()));
            }
            else
            {
                return new[] {new NSubstituteConstructorMethod(type, new ParameterInfo[0])};
            }
        }
    }
}