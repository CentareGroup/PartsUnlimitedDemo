namespace PartsUnlimited.Utils
{
    using System;

    using FeatureSwitch;

    internal class FeatureSwitchFeatureContext : IFeatureContext
    {
        /// <summary>
        /// Determines whether the specified feature is enabled.
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <returns></returns>
        public bool IsEnabled(Type feature)
        {
            return FeatureContext.IsEnabled(feature);
        }

        /// <summary>
        /// Determines whether this instance is enabled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsEnabled<T>() where T : BaseFeature
        {
            return FeatureContext.IsEnabled<T>();
        }
    }
}