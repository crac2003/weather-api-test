using System;
using AutoFixture;
using NUnit.Framework;
using Weather.Domain.Models.Requests;

namespace Weather.Domain.Tests.Models.Requests
{
    [TestFixture]
    public class GetCityRequestTests
    {
        [Test]
        public void WhenIdIsZero_ShouldThrowInvalidOperationException()
        {
            // arrange

            // act
            TestDelegate act = () => GetCityRequest.Create(-100);

            // assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void WhenCreated_ShouldProvideCorrectId()
        {
            // arrange
            var id = new Fixture().Create<int>();

            // act
            var request = GetCityRequest.Create(id);

            // assert
            Assert.That(request.Id, Is.EqualTo(id));
        }
    }
}