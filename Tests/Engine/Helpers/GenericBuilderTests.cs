using System;
using System.Collections.Generic;

using Engine.Helpers;
using Engine.Helpers.Functions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Engine.Helpers
{
    [TestClass]
    public class GenericBuilderTests
    {
        private class TestClassBuilder : GenericBuilder<TestClass>
        {
            public TestClassBuilder() : base(new TestClass())
            {
                DefaultValues = new Dictionary<string, Func<dynamic>>
                {
                    {GenericExtensions.GetPropertyName<TestClass, TestClass>(tc => tc.MeToo), () => new TestClass()}
                };
            }

            public TestClassBuilder(TestClass initial) : this() => ObjectToBuild.SetProperties(initial);
        }

        [TestMethod]
        public void With_SetterProperty_IsExpected()
        {
            // arrange
            var builder = new TestClassBuilder();
            const int expected = 2;

            // act
            var result = builder.With(tc => tc.IAmAProperty, expected).Create();

            // assert
            Assert.AreEqual(expected, result.IAmAProperty);
        }

        [TestMethod]
        public void Create_DefaultValue_IsNotNull()
        {
            // arrange
            var builder = new TestClassBuilder();

            // act
            var result = builder.Create();

            // assert
            Assert.IsNotNull(result.MeToo);
        }

        [TestMethod]
        public void With_GetOnlyProperty_ThrowsKeyNotFoundException()
        {
            // arrange
            var builder = new TestClassBuilder();

            // act
            void SetReadOnlyProperty() => builder.With(tc => tc.ICannotBeWrittenTo, 1.0);

            // assert
            Assert.ThrowsException<KeyNotFoundException>((Action) SetReadOnlyProperty);
        }

        [TestMethod]
        public void With_Field_ThrowsKeyNotFoundException()
        {
            // arrange
            var builder = new TestClassBuilder();

            // act
            void SetField() => builder.With(tc => tc.IAmMostDefinitelyNotAProperty, "test");

            // assert
            Assert.ThrowsException<KeyNotFoundException>((Action) SetField);
        }
    }
}