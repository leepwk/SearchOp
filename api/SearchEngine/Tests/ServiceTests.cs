using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SearchEngine.Common.Model;
using SearchEngine.Service.Interface;
using SearchEngine.Service;
using Shouldly;

namespace SearchEngine.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private Mock<ILogger<WebScraper>> _loggerMock;
        private Mock<IScraperFactory> _scraperFactoryMock;
        private Mock<IScraper> _scraperMock;
        private HttpClient _httpClient;
        private IOptions<AppSettings> _options;
        private AppSettings _appSettings;
        private WebScraper _webScraper;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<WebScraper>>();
            _scraperFactoryMock = new Mock<IScraperFactory>();
            _scraperMock = new Mock<IScraper>();
            _httpClient = new HttpClient();

            _appSettings = new AppSettings
            {
                OverrideTerms = false,
                UrlSearchId = "TestId"
            };
            _options = Options.Create(_appSettings);

            _webScraper = new WebScraper(
                _loggerMock.Object,
                _httpClient,
                _options,
                _scraperFactoryMock.Object
            );
        }

        [Test]
        public async Task FetchByUrlTerms_ReturnResults_ScrapingAllowed()
        {
            // Arrange
            var url = "https://www.google.com/search?q=test";
            var term = "test";
            var expectedResults = new List<SearchEngineResultBase> { Mock.Of<SearchEngineResultBase>() };

            _scraperFactoryMock.Setup(f => f.Create(url)).Returns(_scraperMock.Object);
            _scraperMock.Setup(s => s.IsAllowed()).Returns(true);
            _scraperMock.Setup(s => s.Scrape(url, term, _appSettings.UrlSearchId, false))
                .ReturnsAsync(expectedResults);

            // Act
            var results = await _webScraper.FetchByUrlTerms(url, term, false);

            // Assert
            results.ShouldBe(expectedResults);
            _scraperMock.Verify(s => s.Scrape(url, term, _appSettings.UrlSearchId, false), Times.Once);
        }

        [Test]
        public async Task FetchByUrlTerms_ReturnNoResults_ScrapingNotAllowed()
        {
            // Arrange
            var url = "https://www.google.com/search?q=test";
            var term = "test";

            _scraperFactoryMock.Setup(f => f.Create(url)).Returns(_scraperMock.Object);
            _scraperMock.Setup(s => s.IsAllowed()).Returns(false);

            // Act
            var results = await _webScraper.FetchByUrlTerms(url, term, false);

            // Assert
            results.ShouldBeEmpty();
            _scraperMock.Verify(s => s.Scrape(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("is not allowed to be scraped")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once
            );
        }

        [Test]
        public async Task FetchByUrlTerms_ReturnResults_OverrideTerms()
        {
            // Arrange
            _appSettings.OverrideTerms = true; // override
            var url = "https://www.bing.com/search?q=test";
            var term = "test";
            var expectedResults = new List<SearchEngineResultBase> { Mock.Of<SearchEngineResultBase>() };

            _scraperFactoryMock.Setup(f => f.Create(url)).Returns(_scraperMock.Object);
            _scraperMock.Setup(s => s.IsAllowed()).Returns(false); // ignored
            _scraperMock.Setup(s => s.Scrape(url, term, _appSettings.UrlSearchId, true))
                .ReturnsAsync(expectedResults);

            // Act
            var results = await _webScraper.FetchByUrlTerms(url, term, true);

            // Assert
            results.ShouldBe(expectedResults);
            _scraperMock.Verify(s => s.Scrape(url, term, _appSettings.UrlSearchId, true), Times.Once);
        }
    }
}
