using System;
using System.Collections.Generic;
using System.Linq;
using AutoNSubstitute;
using NSubstitute;
using NSubstitute.Exceptions;
using Ploeh.TestTypeFoundation;
using Xunit;

namespace Ploeh.AutoFixture.AutoNSubstitute.UnitTest
{
    public class FixtureIntegrationTest
    {
        [Fact()]
        public void FixtureDoesNotMockConcreteType()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<Object>();
            // Verify outcome
            IsNotSubstitute(() => result.ToString().Returns("d"));

            // Teardown
        }

        [Fact()]
        public void FixtureAutoMocksInterface()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<IInterface>();
            // Verify outcome
            Assert.NotNull(result);
            Console.Out.WriteLine("result = {0}", result);
            // Teardown
        }

        [Fact()]
        public void FixtureAutoMocksInterfaceCorrectly()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<IInterface>();
            // Verify outcome
            IsSubstitute(() => result.MakeIt(null).Returns(null));
            // Teardown
        }

        private static void IsSubstitute(Action action)
        {
            Assert.DoesNotThrow(() => action());
        }

        private static void IsNotSubstitute(Action action)
        {
            Assert.Throws<CouldNotSetReturnException>(() => action());
        }

        [Fact()]
        public void FixtureAutoMocksAbstractType()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractType>();
            // Verify outcome
            IsSubstitute(() => result.Property4.Returns(null));
            // Teardown
        }

        [Fact()]
        public void FixtureCanCreateGuid()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<Guid>();
            // Verify outcome
            Assert.NotEqual(Guid.Empty, result);
            // Teardown
        }

        [Fact()]
        public void FixtureAutoMocksAbstractTypeWithNonDefaultConstructorRequiringGuid()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractTypeWithNonDefaultConstructor<Guid>>();
            // Verify outcome
            Assert.NotEqual(Guid.Empty, result.Property);
            // Teardown
        }

        [Fact()]
        public void FixtureAutoMocksAbstractTypeWithNonDefaultConstructor()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractTypeWithNonDefaultConstructor<int>>();
            // Verify outcome
            Assert.NotEqual(0, result.Property);
            // Teardown
        }

        [Fact()]
        public void FixtureAutoMocksNestedAbstractTypeWithNonDefaultConstructor()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractTypeWithNonDefaultConstructor<NSubstituteTestTypes.AnotherAbstractTypeWithNonDefaultConstructor<int>>>();
            // Verify outcome
            IsSubstitute(() => result.StringProperty.Returns(""));
            IsSubstitute(() => result.Property.Property2.Returns(""));
            // Teardown
        }

        [Fact()]
        public void FixtureDoesNotMockNestedConcreteTypeWithNonDefaultConstructor()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractTypeWithNonDefaultConstructor<NSubstituteTestTypes.ConcreteGenericType<int>>>();
            // Verify outcome
            IsSubstitute(() => result.StringProperty.Returns(""));
            IsNotSubstitute(() => result.Property.Value.Returns(5));
            // Teardown
        }

        [Fact()]
        public void FixtureDoesNotMockParentOfNestedAbstractTypeWithNonDefaultConstructor()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<NSubstituteTestTypes.ConcreteGenericType<AbstractTypeWithNonDefaultConstructor<int>>>();
            // Verify outcome
            IsNotSubstitute(() => result.Value.Returns(c => null));
            IsSubstitute(() => result.Value.StringProperty.Returns("er"));
            // Teardown
        }

        [Fact()]
        public void FixtureMocksDoubleGenericTypeCorrectly()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<NSubstituteTestTypes.ConcreteDoublyGenericType<ConcreteType, AbstractTypeWithNonDefaultConstructor<int>>>();
            // Verify outcome
            IsNotSubstitute(() => result.Value1.Returns(new ConcreteType()));
            IsNotSubstitute(() => result.Value1.Property4.Returns(new object()));
            IsSubstitute(() => result.Value2.StringProperty.Returns("ds"));
            // Teardown
        }

        [Fact()]
        public void CreateWithAbstractTypeReturnsMockedResult()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractType>();
            // Verify outcome
            IsSubstitute(() => result.Property4.Returns(null));
        }

        [Fact()]
        public void CreateAbstractGenericTypeWithNonDefaultConstructorIsCorrect()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractGenericType<object>>();
            // Verify outcome
            IsSubstitute(() => result.Value.Returns(null));
        }

        [Fact()]
        public void CreateAbstractGenericTypeWithNonDefaultConstructorReturnsCorrectType()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractGenericType<object>>();
            // Verify outcome
            Assert.NotNull(result);
        }

        [Fact()]
        public void CreateAbstractGenericTypeWithConcreteGenericParameterIsCorrect()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<AbstractGenericType<object>>();
            // Verify outcome
            var value = result.Value;
            Assert.NotNull(value);
            IsNotSubstitute(() => value.ToString().Returns(""));
        }

        [Fact()]
        public void FixtureCanCreateList()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<List<ConcreteType>>();
            // Verify outcome
            Assert.False(result.Any());
            // Teardown
        }

        [Fact()]
        public void FixtureCanCreateStack()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<Stack<ConcreteType>>();
            // Verify outcome
            Assert.False(result.Any());
            // Teardown
        }

        [Fact()]
        public void FixtureCanCreateHashSet()
        {
            // Fixture setup
            var fixture = new Fixture().Customize(new AutoNSubstituteMockCustomization());
            // Exercise system
            var result = fixture.CreateAnonymous<HashSet<ConcreteType>>();
            // Verify outcome
            Assert.False(result.Any());
            // Teardown
        }
    }
}
