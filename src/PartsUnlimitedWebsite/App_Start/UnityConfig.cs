using Microsoft.Practices.Unity;
using PartsUnlimited.Models;
using PartsUnlimited.ProductSearch;
using PartsUnlimited.Recommendations;
using PartsUnlimited.Utils;

namespace PartsUnlimited
{
    using FeatureSwitch;

    using PartsUnlimited.FeatureToggles;

    public static class UnityConfig
    {
        public static UnityContainer BuildContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IPartsUnlimitedContext, PartsUnlimitedContext>();
            container.RegisterType<IOrdersQuery, OrdersQuery>();
            container.RegisterType<IRaincheckQuery, RaincheckQuery>();
            //container.RegisterType<IRecommendationEngine, AzureMLFrequentlyBoughtTogetherRecommendationEngine>();
            container.RegisterType<ITelemetryProvider, TelemetryProvider>();
            container.RegisterType<IProductSearch, StringContainsProductSearch>();

            container.RegisterInstance<IHttpClient>(container.Resolve<HttpClientWrapper>());

            return container;
        }

        public static void ConfigureToggleDependencies(IUnityContainer container)
        {
            container.RegisterTypeIfEnabled<ShowRecommendationsToggle, IRecommendationEngine, AzureMLFrequentlyBoughtTogetherRecommendationEngine, NaiveRecommendationEngine>();
        }

        private static void RegisterTypeIfEnabled<TFeature, TInterface, TEnabled, TDisabled>(this IUnityContainer container) 
            where TFeature : BaseFeature
            where TEnabled : TInterface
            where TDisabled : TInterface
        {
            if (FeatureContext.IsEnabled<TFeature>())
            {
                container.RegisterType<TInterface, TEnabled>();
            }
            else
            {
                container.RegisterType<TInterface, TDisabled>();
            }
        }
    }
}