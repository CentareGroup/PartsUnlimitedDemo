using PartsUnlimited.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using PartsUnlimited.Utils;
using PartsUnlimited.ViewModels;

namespace PartsUnlimited.Controllers
{
    using PartsUnlimited.FeatureToggles;

    public class StoreController : Controller
    {
        private readonly IPartsUnlimitedContext db;
        private readonly IFeatureContext featureContext;

        public StoreController(IPartsUnlimitedContext context, IFeatureContext featureContext)
        {
            this.db = context;
            this.featureContext = featureContext;
        }

        //
        // GET: /Store/
        public ActionResult Index()
        {
            var genres = this.db.Categories.ToList();

            return this.View(genres);
        }

        //
        // GET: /Store/Browse?genre=Disco
        public ActionResult Browse(int categoryId)
        {
            // Retrieve Category genre and its Associated associated Products products from database
            var genreModel = this.db.Categories.Include("Products").Single(g => g.CategoryId == categoryId);

            return this.View(genreModel);
        }

        public ActionResult Details(int id)
        {

            var productCacheKey = $"product_{id}";
            var product = MemoryCache.Default[productCacheKey] as Product;
            if (product == null)
            {
                product = this.db.Products.Single(a => a.ProductId == id);
                //Remove it from cache if not retrieved in last 10 minutes
                MemoryCache.Default.Add(productCacheKey, product, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(10) });
            }
            var viewModel = new ProductViewModel
            {
                Product = product,
                ShowRecommendations = this.featureContext.IsEnabled<ShowRecommendationsToggle>()
            };

            return this.View(viewModel);
        }

    }
}
