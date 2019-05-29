using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Weather.Domain.Models;
using Weather.Domain.Models.Requests;
using Weather.Domain.Services;

namespace Weather.Domain.Tests.Services
{
    [TestFixture]
    public class WeatherServiceTests
    {
        private IFixture _fixture = new Fixture();

        private Mock<IWeatherDataProvider> _weatherDataProviderMock;
        private Mock<IBackupService> _backupServiceMock;

        private IWeatherService _subject;

        private GetCityRequest _request;

        [SetUp]
        public void Setup()
        {
            _weatherDataProviderMock = new Mock<IWeatherDataProvider>();
            _backupServiceMock = new Mock<IBackupService>();

            _request = GetCityRequest.Create(_fixture.Create<int>());

            _subject = new WeatherService(_weatherDataProviderMock.Object, _backupServiceMock.Object);
        }

        [Test]
        public async Task WhenCalled_ShouldCallWeatherProvider()
        {
            // arrange

            // act
            await _subject.GetAsync(new[] { _request });

            // assert
            _weatherDataProviderMock.Verify(x => x.GetCityForecastAsync(_request.Id), Times.Once);
        }

        [Test]
        public async Task WhenCalled_ShouldCacheTheResult()
        {
            // arrange

            // act
            await _subject.GetAsync(new[] { _request });

            // assert
            _weatherDataProviderMock.Verify(x => x.GetCityForecastAsync(_request.Id), Times.Once);
        }

        [Test]
        public async Task WhenResultWasCached_ShouldReturnCorrectModel()
        {
            // arrange
            var cached = _fixture.Create<City>();
            _backupServiceMock.Setup(x => x.ReadAsync<City>(It.IsAny<string>()))
                .Returns(Task.FromResult(cached));

            // act
            var result = await _subject.GetAsync(new[] { _request });

            // assert
            var list = result.ToList();
            _weatherDataProviderMock.Verify(x => x.GetCityForecastAsync(_request.Id), Times.Never);
            Assert.That(list[0], Is.EqualTo(cached));
        }
    }
}
