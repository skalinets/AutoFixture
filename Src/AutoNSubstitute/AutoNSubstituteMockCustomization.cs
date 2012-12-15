using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace AutoNSubstitute
{
    public class AutoNSubstituteMockCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException("fixture");
            }
            var invoker = new MethodInvoker(new NSubstituteConstructorQuery());
            var advice = new NSubstituteAroundAdvice(invoker);
            fixture.ResidueCollectors.Add(advice);
        }
    }
}