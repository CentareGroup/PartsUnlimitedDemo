namespace PartsUnlimited
{
    using FeatureSwitch;
    using FeatureSwitch.Unity;

    using Microsoft.Practices.Unity;

    using Utils;

    public static class ToggleConfig
    {
        /// <summary>
        /// Sets up the specified feature toggle system with a container.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        public static void Setup(IUnityContainer container)
        {
            var builder = new FeatureSetBuilder(new UnityDependencyContainer(container));
            builder.Build();

            container.RegisterType<IFeatureContext, FeatureSwitchFeatureContext>(
                new ContainerControlledLifetimeManager());
        }
    }
}
