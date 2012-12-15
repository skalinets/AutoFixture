using System;
using System.Collections.Generic;
using System.Linq;
using AutoNSubstitute;
using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Ploeh.AutoFixture.AutoNSubstitute.UnitTest
{
    public class NSubstituteCustomizationTest
    {
        [Fact]
        public void SutImplementsICustomization()
        {
            // Fixture setup
            // Exercise system
            var sut = new AutoNSubstituteMockCustomization();
            // Verify outcome
            Assert.IsAssignableFrom<ICustomization>(sut);
            // Teardown
        }

        [Fact]
        public void CustomizeNullFixtureThrows()
        {
            // Fixture setup
            var sut = new AutoNSubstituteMockCustomization();
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Customize(null));
            // Teardown
        }

        [Fact]
        public void CustomizeAddsAppropriateResidueCollectors()
        {
            // Fixture setup
            var residueCollectors = new List<ISpecimenBuilder>();
            var fixtureStub = Substitute.For<IFixture>();
            fixtureStub.ResidueCollectors.Returns(residueCollectors);

            var sut = new AutoNSubstituteMockCustomization();
            // Exercise system
            sut.Customize(fixtureStub);
            // Verify outcome
            var postprocessor = residueCollectors.OfType<NSubstituteAroundAdvice>().Single();
            var ctorInvoker = Assert.IsAssignableFrom<MethodInvoker>(postprocessor.Builder);
            Assert.IsAssignableFrom<NSubstituteConstructorQuery>(ctorInvoker.Query);
            // Teardown
        }
    }
}
