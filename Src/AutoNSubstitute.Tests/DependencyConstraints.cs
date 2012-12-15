using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace AutoNSubstitute.Tests
{
    public class DependencyConstraints
    {
        [Theory]
        [InlineData("FakeItEasy")]
        [InlineData("Moq")]
        [InlineData("Rhino.Mocks")]
        [InlineData("xunit")]
        [InlineData("xunit.extensions")]
        public void AutoFixtureDoesNotReference(string assemblyName)
        {
            // Fixture setup
            // Exercise system
            var references = typeof(AutoNSubstituteMockCustomization).Assembly.GetReferencedAssemblies();
            // Verify outcome
            Assert.False(references.Any(an => an.Name == assemblyName));
            // Teardown
        }

        [Theory]
        [InlineData("FakeItEasy")]
        [InlineData("Rhino.Mocks")]
        [InlineData("Moq")]
        public void AutoFixtureUnitTestsDoNotReference(string assemblyName)
        {
            // Fixture setup
            // Exercise system
            var references = this.GetType().Assembly.GetReferencedAssemblies();
            // Verify outcome
            Assert.False(references.Any(an => an.Name == assemblyName));
            // Teardown
        }
    }
}