using Greenfinch.Core.Models.Newsletter;
using Greenfinch.Core.Services;
using Greenfinch.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Greenfinch.Tests.Core.Services
{
    public class NewsletterServiceUnitTest
    {
        private ApiContext _db;
        private readonly Mock<ILogger> _logger;
        public NewsletterServiceUnitTest(ITestOutputHelper testOutputHelper)
        {

            DbContextOptions<ApiContext> dbOptions = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _db = new ApiContext(dbOptions);
            _logger = new Mock<ILogger>();
            _logger.Setup(l => l.ForContext<NewsletterService>()).Returns(_logger.Object);
            _logger.Setup(l => l.Information(It.IsAny<string>(), It.IsAny<object[]>()));
        }

        private NewsletterService CreateServiceInstance()
        {
            return new NewsletterService(_db, _logger.Object);
        }

        [Theory]
        [MemberData(nameof(GetSubscriptionTestData))]
        public async Task ValidateSubscription(bool isValid, Subscription subscription)
        {
            var service = CreateServiceInstance();
            var result = await service.ValidateSubscription(subscription);
            Assert.Equal(isValid, result.isValid);

        }

        [Fact]
        public async Task ValidateSubscription_EmailAlreadyExistsInDb()
        {
            var service = CreateServiceInstance();
            Subscription subscription = new Subscription() { Email = "test@test.de", Reason = "test", Referrer = "TestRef", StartDate = DateTime.UtcNow };
            await _db.Subscriptions.AddAsync(subscription);
            await _db.SaveChangesAsync();

            var result = await service.ValidateSubscription(subscription);
            Assert.False(result.isValid);
            Assert.Contains(result.Errors, e => e.Control.ToLower() == nameof(subscription.Email).ToLower());
            Assert.Contains(result.Errors, e => e.ErrorKeys.Contains("existingEmail"));
        }
        public static IEnumerable<object[]> GetSubscriptionTestData()
        {
            yield return new object[] { false, null };
            yield return new object[] { false, new Subscription() };
            yield return new object[] { true, new Subscription() { Email = "test@test.com", Reason = "test", Referrer = "Advert", StartDate = DateTime.UtcNow } };
            yield return new object[] { true, new Subscription() { Email = "test@test.com", Reason = null, Referrer = "Advert", StartDate = DateTime.UtcNow } };
            yield return new object[] { false, new Subscription() { Email = "test@test.com", Reason = "test", Referrer = "", StartDate = DateTime.UtcNow } };
            yield return new object[] { false, new Subscription() { Email = "notanemailadress", Reason = "test", Referrer = "", StartDate = DateTime.UtcNow } };
        }

        [Fact]
        public async Task Subscribe()
        {
            var service = CreateServiceInstance();
            Subscription subscription = new Subscription() { Email = "test@test.de", Reason = "test", Referrer = "TestRef", StartDate = DateTime.UtcNow };
            var result = await service.Subscribe(subscription);
            Assert.True(result.Data);
            Assert.True(_db.Subscriptions.Count() == 1);
        }
    }
}
