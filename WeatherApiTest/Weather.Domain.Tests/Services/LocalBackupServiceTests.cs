using System.IO;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Weather.Domain.Services;

namespace Weather.Domain.Tests.Services
{
    [TestFixture]
    public class LocalBackupServiceTests
    {
        private IFixture _fixture = new Fixture();

        private Mock<ILocalBackupConfig> _configMock;
        private Mock<IFileSystemService> _fileSystemMock;

        private IBackupService _subject;

        [SetUp]
        public void Setup()
        {
            _configMock = new Mock<ILocalBackupConfig>();
            _fileSystemMock = new Mock<IFileSystemService>();

            _subject = new LocalBackupService(_configMock.Object, _fileSystemMock.Object);
        }

        [Test]
        public async Task ReadWhenCalled_ShouldPassCorrectPath()
        {
            // arrange
            var name = _fixture.Create<string>();
            var folder = _fixture.Create<string>();
            _configMock.Setup(x => x.BackupFolder).Returns(folder);

            // act
            await _subject.ReadAsync<object>(name);

            // assert
            _fileSystemMock.Verify(x => x.ReadAsync<object>(It.Is<string>(s => Path.Combine(folder, name) == s)));
        }

        [Test]
        public async Task SaveWhenCalled_ShouldPassCorrectPath()
        {
            // arrange
            var name = _fixture.Create<string>();
            var folder = _fixture.Create<string>();
            var obj = _fixture.Create<object>();
            _configMock.Setup(x => x.BackupFolder).Returns(folder);

            // act
            await _subject.SaveAsync(name, obj);

            // assert
            _fileSystemMock.Verify(x => x.SaveAsync(It.Is<string>(s => Path.Combine(folder, name) == s), obj));
        }
    }
}