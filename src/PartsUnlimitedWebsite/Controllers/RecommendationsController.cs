using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using PartsUnlimited.FeatureToggles;
using PartsUnlimited.Models;
using PartsUnlimited.Recommendations;
using PartsUnlimited.Utils;

namespace PartsUnlimited.Controllers
{
    public class RecommendationsController : Controller
    {
        private readonly IPartsUnlimitedContext db;
        private readonly IFeatureContext featureContext;
        private readonly IRecommendationEngine recommendation;
        
        public RecommendationsController(IPartsUnlimitedContext context, IRecommendationEngine recommendationEngine, IFeatureContext featureContext)
        {
            this.db = context;
            this.recommendation = recommendationEngine;
            this.featureContext = featureContext;
        }

        public async Task<ActionResult> GetRecommendations(string productId)
        {
            if (!this.featureContext.IsEnabled<ShowRecommendationsToggle>())
            {
                return new EmptyResult();
            }

            var recommendedProductIds = await this.recommendation.GetRecommendationsAsync(productId);

            var recommendedProducts = await this.db.Products.Where(x => recommendedProductIds.Contains(x.ProductId.ToString())).ToListAsync();

            return this.PartialView("_Recommendations", recommendedProducts);
        }
    }
}
