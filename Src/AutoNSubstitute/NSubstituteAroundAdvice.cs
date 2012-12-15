using System;
using NSubstitute;
using Ploeh.AutoFixture.Kernel;

namespace AutoNSubstitute
{
    public class NSubstituteAroundAdvice : ISpecimenBuilder
    {

        public NSubstituteAroundAdvice(ISpecimenBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            Builder = builder;
        }

        public ISpecimenBuilder Builder { get; private set; }

        public object Create(object request, ISpecimenContext context)
        {
            if (!IsMockable(request))
            {
                return new NoSpecimen(request);
            }
            return Builder.Create(request, context);
//            return Substitute.For(new[] {(Type) request}, context.);
            Console.Out.WriteLine("request = {0}", request);
//            return context.Resolve(request);
            return new NoSpecimen(request);
        }

        internal static bool IsMockable(object request)
        {
            var t = request as Type;
            if (t == null)
            {
                return false;
            }

            return (t.IsInterface || t.IsAbstract);
        }
    }
}