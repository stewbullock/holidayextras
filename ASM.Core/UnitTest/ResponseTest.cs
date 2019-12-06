#if DEBUG
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ASM.Core.UnitTest
{
    class ResponseTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void CreateExpectSuccessWithEmptyResponses()
        {
            // Arrange
            var responses = new List<IResponse>();

            // Act
            var result = Response.Create(responses);

            // Assert
            Assert.IsTrue(result.ResponseState == ResponseState.Success);
        }

        [Test]
        public void CreateExpectSuccessWithOnlySuccessfullResponses()
        {
            // Arrange
            var responses = new List<IResponse>
            {
                Response.Success(),
                Response.Success()
            };

            // Act
            var result = Response.Create(responses);

            // Assert
            Assert.IsTrue(result.ResponseState == ResponseState.Success);
        }

        [Test]
        public void CreateExpectFailureWithOnlyFailedResponses()
        {
            // Arrange
            var responses = new List<IResponse>
            {
                Response.Failed("Test Failure 1"),
                Response.Failed("Test Failure 2")
            };

            // Act
            var result = Response.Create(responses);

            // Assert
            Assert.IsTrue(result.ResponseState == ResponseState.Failed);
            Assert.AreEqual("Test Failure 1\r\nTest Failure 2", result.Failures[0]);
        }

        [Test]
        public void CreateExpectFailureWithSuccessfullAndFailedResponses()
        {
            // Arrange
            var responses = new List<IResponse>
            {
                Response.Failed("Test Failure 1"),
                Response.Success()
            };

            // Act
            var result = Response.Create(responses);

            // Assert
            Assert.IsTrue(result.ResponseState == ResponseState.Failed);
            Assert.AreEqual("Test Failure 1\r\n", result.Failures[0]);
        }

        [Test]
        public void FailExpectFailedResponse()
        {
            // Arrange
            var response = new Response();

            // Act
            response.Fail("Test Failure 1");
            response.Fail("Test Failure 2");

            // Assert
            Assert.IsTrue(response.ResponseState == ResponseState.Failed);
            Assert.AreEqual("Test Failure 1", response.Failures[0]);
            Assert.AreEqual("Test Failure 2", response.Failures[1]);
        }

        [Test]
        public void SuccessExpectSuccessfulResponseWithMessage()
        {
            // Arrange
            var response = Response.Success("Test Success Message");

            // Act

            // Assert
            Assert.IsTrue(response.ResponseState == ResponseState.Success);
            Assert.AreEqual("Test Success Message", response.SuccessMessage);
        }

        [Test]
        public void SuccessExpectSuccessfulResponseWithoutMessage()
        {
            // Arrange
            var response = Response.Success();

            // Act

            // Assert
            Assert.IsTrue(response.ResponseState == ResponseState.Success);
            Assert.IsNull(response.SuccessMessage);
        }

        [Test]
        public void GenericSuccessExpectSuccessfullResponseEncapsulatingResponseObject()
        {
            // Arrange
            var testObject = new Object();

            // Act
            var response = Response<Object>.Success(testObject);

            // Assert
            Assert.IsTrue(response.ResponseState == ResponseState.Success);
            Assert.AreSame(testObject, response.ResponseObject);
        }

        [Test]
        public void GenericFailureExpectFailedResponseNotEncapsulatingResponseObject()
        {
            // Act
            var response = Response<Object>.Failed("Test Failure");

            // Assert
            Assert.IsTrue(response.ResponseState == ResponseState.Failed);
            Assert.AreEqual("Test Failure", response.Failures[0]);
            Assert.AreSame(null, response.ResponseObject);
        }
    }
}
#endif