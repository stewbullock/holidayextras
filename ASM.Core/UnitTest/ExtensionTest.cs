#if DEBUG
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ASM.Core.UnitTest
{
    class ExtensionTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void AggregateExpectFailedResponsesAggregatedIntoOneFailedMessage()
        {
            // Arrange
            var response1 = Response.Failed(new List<string> { "Test Failure 1", "Test Failure 2" });
            var response3 = Response.Failed("Test Failure 3");
            var response4 = Response.Failed("Test Failure 4");

            var responses = new List<IResponse>
            {
                response1,
                response3,
                response4
            };

            // Act
            var result = responses.Aggregate();

            // Assert
            Assert.AreEqual("Test Failure 1\r\nTest Failure 2\r\nTest Failure 3\r\nTest Failure 4", result);
        }

        [Test]
        public void AggregateExpectFailedResponseAggregatedIntoOneFailedMessage()
        {
            // Arrange
            var response = Response.Failed(new List<string> { "Test Failure 1", "Test Failure 2", "Test Failure 3", "Test Failure 4" });

            // Act
            var result = response.Aggregate();

            // Assert
            Assert.AreEqual("Test Failure 1\r\nTest Failure 2\r\nTest Failure 3\r\nTest Failure 4", result);
        }

        [Test]
        public void FailedExpectFailedResponseStateWhenResponsesContainOneFailedResponse()
        {
            // Arrange
            var response1 = Response.Failed(new List<string> { "Test Failure 1", "Test Failure 2" });
            var response3 = Response.Success();
            var response4 = Response.Success();

            var responses = new List<IResponse>
            {
                response1,
                response3,
                response4
            };

            // Act
            var result = responses.Failed();

            // Assert
            Assert.IsTrue(ResponseState.Failed == result);
        }

        [Test]
        public void FailedExpectSuccessResponseStateWhenResponsesContainNoFailedResponse()
        {
            // Arrange
            var response1 = Response.Success();
            var response3 = Response.Success();
            var response4 = Response.Success();

            var responses = new List<IResponse>
            {
                response1,
                response3,
                response4
            };

            // Act
            var result = responses.Failed();

            // Assert
            Assert.IsTrue(ResponseState.Success == result);
        }

        [Test]
        public void ContainsReturnTrueWhenPartStringExists()
        {
            // Arrange
            const string source = "This is a test sentence";

            // Act
            var result = source.Contains("a test sent", StringComparison.InvariantCultureIgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsReturnTrueWhenFullStringExists()
        {
            // Arrange
            const string source = "This is a test sentence";

            // Act
            var result = source.Contains("This is a test sent", StringComparison.InvariantCultureIgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsReturnFalseWhenStringDoesNotExistInAnyPart()
        {
            // Arrange
            const string source = "This is a test sentence";

            // Act
            var result = source.Contains("a different sentence", StringComparison.InvariantCultureIgnoreCase);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
#endif