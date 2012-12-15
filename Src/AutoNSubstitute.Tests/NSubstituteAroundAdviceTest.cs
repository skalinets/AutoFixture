using System;
using AutoNSubstitute;
using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.AutoFixture.AutoNSubstitute.UnitTest
{
    public class NSubstituteAroundAdviceTest
    {
        [Fact]
        public void SutImplementsISpecimenBuilder()
        {
            // Fixture setup
            var dummyBuilder = Substitute.For<ISpecimenBuilder>();
            // Exercise system
            var sut = new NSubstituteAroundAdvice(dummyBuilder);
            // Verify outcome
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
            // Teardown
        }

        [Fact]
        public void InitializeWithNullBuilderThrows()
        {
            // Fixture setup
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new NSubstituteAroundAdvice((ISpecimenBuilder)null));
            // Teardown
        }

        [Fact]
        public void BuilderIsCorrect()
        {
            // Fixture setup
            var expectedBuilder = Substitute.For<ISpecimenBuilder>();
            var sut = new NSubstituteAroundAdvice(expectedBuilder);
            // Exercise system
            ISpecimenBuilder result = sut.Builder;
            // Verify outcome
            Assert.Equal(expectedBuilder, result);
            // Teardown
        }

        [Theory]
        [InlineData("")]
        [InlineData(1)]
        [InlineData(typeof(object))]
        [InlineData(typeof(string))]
        public void CreateWithNonMockRequestReturnsCorrectResult(object request)
        {
            // Fixture setup
            var dummyBuilder = Substitute.For<ISpecimenBuilder>();
            var sut = new NSubstituteAroundAdvice(dummyBuilder);
            // Exercise system
            var dummyContext = Substitute.For<ISpecimenContext>();
            var result = sut.Create(request, dummyContext);
            // Verify outcome
            var expectedResult = new NoSpecimen(request);
            Assert.Equal(expectedResult, result);
            // Teardown
        }
    }
}
