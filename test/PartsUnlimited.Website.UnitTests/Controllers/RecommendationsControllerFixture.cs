using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PartsUnlimited.Website.UnitTests.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Moq;

    using PartsUnlimited.Controllers;
    using PartsUnlimited.FeatureToggles;
    using PartsUnlimited.Models;
    using PartsUnlimited.Recommendations;
    using PartsUnlimited.Utils;
    using PartsUnlimited.Website.UnitTests.Helpers;

    [TestClass]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RecommendationsControllerFixture
    {
        [TestMethod]
        public async Task TestGetRecommendations_WhenFeatureDisabled_ReturnsEmptySet()
        {
            var dbMock = new Mock<IPartsUnlimitedContext>(MockBehavior.Strict);
            var featureContext = new Mock<IFeatureContext>(MockBehavior.Strict);
            featureContext.Setup(f => f.IsEnabled<ShowRecommendationsToggle>()).Returns(false);

            var recommendationEngine = new Mock<IRecommendationEngine>(MockBehavior.Strict);

            var controller = new RecommendationsController(dbMock.Object, recommendationEngine.Object, featureContext.Object);

            var result = await controller.GetRecommendations("1234");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EmptyResult));

            dbMock.VerifyAll();
            featureContext.VerifyAll();
            recommendationEngine.VerifyAll();
        }

        [TestMethod]
        public async Task TestGetRecommendations_WhenFeatureEnabledAndIsValid_ReturnsRelatedProducts()
        {
            var product1 = new Product { ProductId = 4567 };
            var product2 = new Product { ProductId = 1212 };
            var product3 = new Product { ProductId = 9999 };
            var data = new List<Product>
            {
                product1,
                product2,
                product3,
            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Product>(data.Provider));
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.As<IDbAsyncEnumerable<Product>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Product>(data.GetEnumerator()));

            var dbMock = new Mock<IPartsUnlimitedContext>(MockBehavior.Strict);
            dbMock.Setup(d => d.Products).Returns(mockSet.Object);

            var featureContext = new Mock<IFeatureContext>(MockBehavior.Strict);
            featureContext.Setup(f => f.IsEnabled<ShowRecommendationsToggle>()).Returns(true);

            var recommendationEngine = new Mock<IRecommendationEngine>(MockBehavior.Strict);
            recommendationEngine.Setup(r => r.GetRecommendationsAsync("1234")).ReturnsAsync(new[] { "4567", "1212" });

            var controller = new RecommendationsController(dbMock.Object, recommendationEngine.Object, featureContext.Object);

            var result = await controller.GetRecommendations("1234");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));

            var partialViewResult = (PartialViewResult)result;
            var model = partialViewResult.Model as List<Product>;
            
            CollectionAssert.Contains(model, product1);
            CollectionAssert.Contains(model, product2);
            CollectionAssert.DoesNotContain(model, product3);

            dbMock.VerifyAll();
            featureContext.VerifyAll();
            recommendationEngine.VerifyAll();
        }
    }
}
