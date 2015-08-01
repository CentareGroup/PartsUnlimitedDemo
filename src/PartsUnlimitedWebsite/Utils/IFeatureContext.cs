namespace PartsUnlimited.Utils
{
    using System;

    using FeatureSwitch;

    /// <summary>
    /// An interface that supplies the system with feature information.
    /// </summary>
    public interface IFeatureContext
    {
        /// <summary>
        /// Determines whether the specified feature is enabled.
        /// </summary>
        /// <param name="feature">The feature.</param>
        /// <returns></returns>
        bool IsEnabled(Type feature);

        /// <summary>
        /// Determines whether this instance is enabled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool IsEnabled<T>() where T : BaseFeature;
    }
}